using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float bulletSpeed = 15;
    public float bulletDamage = 10f;

    public Rigidbody2D rb;

    private void Update() {
        GameObject planet = GameObject.FindWithTag("ProjectileSource");
        transform.position = Vector3.MoveTowards(transform.position, planet.transform.position,   bulletSpeed*Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        GameObject player = GameObject.FindWithTag("Player");
        if (player.GetInstanceID() != other.gameObject.GetInstanceID()) {
            Debug.Log("Bullet making contact");
            Destroy(gameObject);
        } 
        Debug.Log(other.gameObject.name);
    }
    
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
