using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour, IShootable
{

    private bool m_Shooting = false;
    private Vector2 m_Direction;
    private float projSpeed;
    

    //iirc this should make cullingdistance consistent across all bullets
    public static float cullingDistance = 17;

    void OnEnable()
    {
        Invoke("Recycle", 5f);
    }
    // Update is called once per frame
    void Update()
    {

        if (m_Shooting)
        {
            transform.position += (Vector3)m_Direction * Time.deltaTime * projSpeed;
        }
        if(Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("ProjectileSource").transform.position) > cullingDistance)
        {
            Debug.Log("Culling");
            CancelInvoke();
            Recycle();
        }
    }

    void Recycle()
    {
        gameObject.SetActive(false);
    }

    public void Shoot(float speed, Vector3 target, float launchAngle)
    {
        m_Shooting = true;
        projSpeed = speed;
        m_Direction = UtilFunctions.DegreeToVector2(launchAngle);
    }
}
