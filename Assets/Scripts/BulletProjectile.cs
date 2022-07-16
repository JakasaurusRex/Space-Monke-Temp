using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour, IShootable
{

    private bool m_Shooting = false;
    private Vector2 m_Direction;
    private float projSpeed;
    public void Shoot(float speed)
    {
        m_Direction = new Vector2(Random.value, Random.value).normalized;
        m_Shooting = true;
        projSpeed = speed;
    }

    public void ShootAngle(float speed, float launchAngle)
    {
        throw new System.NotImplementedException();
    }

    public void ShootTargeted(float speed, Vector3 target)
    {
        throw new System.NotImplementedException();
    }
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
    }

    void Recycle()
    {
        gameObject.SetActive(false);
    }
}
