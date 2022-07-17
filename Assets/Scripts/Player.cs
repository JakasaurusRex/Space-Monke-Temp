using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.UI;
using UnityEngine;

public class Player : MonoBehaviour {
    
    //Rotation around planet code
    public float movementSpeed = 600f;
    public Transform planet;
    private float movement = 0f;
    [SerializeField] private float radius;
    private float angleAroundPlanet;
    public float minClamp;
    public float maxClamp;

    //Facing towards the planet
    public float rotationModifier;
    public float rotSpeed;

    private void Start() {
        angleAroundPlanet = 90;
    }

    void Update() {
        //Player movement
        movement = Input.GetAxisRaw("Horizontal");
        angleAroundPlanet = Mathf.Clamp(angleAroundPlanet + movement * Time.deltaTime * -movementSpeed, minClamp, maxClamp);
        Vector3 pos = (radius * UtilFunctions.DegreeToVector2(angleAroundPlanet));
        transform.position = pos + planet.position;

        //Facing planet code
        Vector3 vectorToPlanet = transform.position - planet.transform.position;
        float angle = Mathf.Atan2(vectorToPlanet.y, vectorToPlanet.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotSpeed);
    }
    
}
