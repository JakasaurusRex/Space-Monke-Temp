using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    
    //Rotation around planet code
    public float movementSpeed = 600f;
    public Transform planet;
    private float movement = 0f;
    [SerializeField] private float radius;
    private float angleAroundPlanet;
    public float minClamp;
    public float maxClamp;

    private PlayerControls _playerActions;

    //Facing towards the planet
    public float rotationModifier;
    public float rotSpeed;

    
    private void Awake() {
        _playerActions = new PlayerControls();
    }

    private void OnEnable() {
        _playerActions.EncounterScene.Enable();
    }
    private void OnDisable() {
        _playerActions.EncounterScene.Disable();
    }

    private void Start() {
        angleAroundPlanet = 90;
    }

    void Update() {
        //Player movement
        movement = _playerActions.EncounterScene.Movement.ReadValue<float>();
        Debug.Log(movement);
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
