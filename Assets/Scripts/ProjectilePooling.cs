using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooling : MonoBehaviour
{

    public static ProjectilePooling SharedInstance;
   
    private List<List<GameObject>> _pools = new();
    
    public List<GameObject> Projectiles;
    public int amountToPool = 100;

    void Awake()
    {
        SharedInstance = this;
        //iterate through all projectile types
        for (int i = 0; i < Projectiles.Count; i++)
        {
            //create a pool for it
            _pools.Add(new List<GameObject>());
            GameObject tmp;
            //populate with that projectile
            for (int j = 0; j < amountToPool; j++)
            {
                tmp = Instantiate(Projectiles[i]);
                tmp.SetActive(false);
                _pools[i].Add(tmp);
            }
        }
    }

    //kinda roundabout but does work
    public GameObject GetPooledObject(int index)
    {
        if(index > _pools.Count)
        {
            Debug.LogError("Invalid retrieval index");
            return null;
        }

        for (int i = 0; i < amountToPool; i++)
        {
            if (!_pools[index][i].activeInHierarchy)
            {
                return _pools[index][i];
            }
        }
        //no available objects
        Debug.LogError("No objects found! Increase the pool size or check your spelling");
        return null;
    }
}
