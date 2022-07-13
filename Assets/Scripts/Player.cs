using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float movementSpeed = 600f;

    public Transform planet;
    
    private float movement = 0f;
    
    // Update is called once per frame
    void Update() {
        movement = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate() {
        transform.RotateAround(new Vector3(planet.position.x, planet.position.y, 0), Vector3.forward, movement * Time.deltaTime * -movementSpeed);
    }
    
}
