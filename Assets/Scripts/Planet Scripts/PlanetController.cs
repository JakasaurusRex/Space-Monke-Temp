using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//individual groups of projectiles
[System.Serializable]
public class Attack
{
    public string AttackName;
    public float AttackDelay;
    //To expand the attack system, this may graduate 
    public int ProjectileIndex;
    public int AttackNumber;
    [Tooltip("For debugging purposes, or perhaps a combat log. Should be pretty.")]
    public string Descriptor;

}

//phases of combat
[System.Serializable]
public class Wave
{
    public string Name; 
    public List<Attack> Attacks;
}

public class PlanetController : MonoBehaviour,DamagableObject {
    
    private float health;
    public float maxHealth;
    public List<Wave> waves;
    private int currentWave;
    public bool dead;

    [Tooltip("Tweak this to fit the perspective")]
    [Range(0, 180)]
    public int spawnAngle = 60;
    [Tooltip("Use this to help tweak the perspective")]
    [Range(0, 180)]
    public float spawnAngleOffset;
    public bool autoChooseOffset = true;
    [Tooltip("This loosely tunes the entire encounter system. Use other variables for finer control")]
    public float difficultyTuner = 1;

    [Tooltip("How frequently to spawn waves")]
    public float AttackCooldown = 3;

    public float spawnRadius = 15;

    [Tooltip("How much randomness is in each variable where it makes sense")]
    [Range(0f, 1f)]
    public float randomness = .20f;
    
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, BulletProjectile.cullingDistance);
    }
    
    // Start is called before the first frame update
    void Start() {
        health = maxHealth;
        currentWave = 0;
        StartCoroutine(SpawnWave());
        if (autoChooseOffset)
        {
            float offset = (180 - spawnAngle) / 2;
            if (offset <= 180 && offset >= 0)
            {
                spawnAngleOffset = offset;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0) {

            Debug.Log("I am dead");
            StopAllCoroutines();
            //dead = true; this or netx line
            //Destroy(this.gameObject);
            
        }

        if (health < (maxHealth - (currentWave / waves.Count * maxHealth)))
        {
            Debug.Log("Increasing wave");
            currentWave++;
        }
    }

    public void Damage(float damage) {
        health -= damage;
    }

    IEnumerator SpawnWave()
    {
        Debug.Log("Spawning Wave");
        foreach(Attack attack in waves[currentWave].Attacks)
        {
            for (int i = 0; i < attack.AttackNumber; i++)
            {
                GameObject tmp = ProjectilePooling.SharedInstance.GetPooledObject(attack.ProjectileIndex);
                var angleVector = UtilFunctions.DegreeToVector2(spawnAngleOffset + (i * spawnAngle / attack.AttackNumber));
                Vector2 source = (Vector2)transform.position + (spawnRadius * angleVector);
                //guard against an empty pool
                if(tmp != null) {
                    tmp.SetActive(true);
                    tmp.transform.position = source;
                    IShootable shootInterface = tmp.GetComponent<IShootable>();
                    if (shootInterface != null)
                    {
                        shootInterface.Shoot(10f, Vector3.zero, (i * spawnAngle / attack.AttackNumber) + spawnAngleOffset);
                    }
                    if (attack.AttackDelay == 0)
                    {
                        continue;
                    }
                }

                yield return new WaitForSeconds(attack.AttackDelay);
            }

        }
        yield return new WaitForSeconds(AttackCooldown * Random.Range(1 - randomness, 1 + randomness));
        StartCoroutine(SpawnWave());
    }
}
