using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObject : MonoBehaviour {
    
    public float maxHealth;
    public float currentHealth;
    public float phases;
    public float phase;


    private void Start() {
        currentHealth = maxHealth;
        phase = 1;
        StartCoroutine(waiter());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentHealth < 0) {
            Destroy(this.gameObject);
        }
        
        if (currentHealth < (maxHealth - (phase / phases) * maxHealth)) {
            phase++;
        } 
    }
    
    IEnumerator waiter()
    {
        //Wait for 1 seconds
        while (true) {
            Damage(1);
            yield return new WaitForSeconds(0.5f); 
        }
    }

    void Damage(float damage) {
        currentHealth -= damage;
    }
}
