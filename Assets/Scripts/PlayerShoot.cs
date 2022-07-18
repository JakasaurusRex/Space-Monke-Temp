using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    public float fireRate = 0.2f;
    public Transform firingPoint;
    public GameObject bulletPrefab;

    private PlayerControls _playerActions;
    
    private float timeUntilFire;

    private void Awake() {
        _playerActions = new PlayerControls();
    }

    private void OnEnable() {
        _playerActions.EncounterScene.Enable();
    }
    private void OnDisable() {
        _playerActions.EncounterScene.Disable();
    }

    private void Update() {
        float didShoot = _playerActions.EncounterScene.Shoot.ReadValue<float>();
        if (didShoot != 0 && timeUntilFire < Time.time) {
            shoot();
            timeUntilFire = Time.time + fireRate;
        }

        void shoot() {
            float angle = 270f;
            Instantiate(bulletPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0f, 0f, angle)));
        }
    }
}
