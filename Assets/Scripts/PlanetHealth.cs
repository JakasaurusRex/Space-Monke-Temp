using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHealth : MonoBehaviour,DamagableObject {
    
    public float health;
    public float maxHealth;
    public float stages;
    public float stage;
    public bool dead;
    
    // Start is called before the first frame update
    void Start() {
        health = maxHealth;
        stage = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health < 0) {
            //dead = true; this or netx line
            Destroy(this.gameObject);
        }

        if (health < (maxHealth - ((stage / stages) * maxHealth))) {
            stage++;
        }
    }

    public void Damage(float damage) {
        health -= damage;
    }
}
