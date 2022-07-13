using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float bulletSpeed = 15;
    public float bulletDamage = 10f;

    public Rigidbody2D rb;

    private void FixedUpdate() {
        rb.velocity = Vector2.down * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //what to do when enterijng collision
        Destroy(gameObject);
    }
}
