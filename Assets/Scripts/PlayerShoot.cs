using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    public float fireRate = 0.2f;
    public Transform firingPoint;
    public GameObject bulletPrefab;

    private float timeUntilFire;

    private void Update() {
        if (Input.GetKeyDown("space") && timeUntilFire < Time.time) {
            shoot();
            timeUntilFire = Time.time + fireRate;
        }

        void shoot() {
            float angle = 270f; 
            Instantiate(bulletPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0f, 0f, angle)));
        }
    }
}
