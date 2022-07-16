using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable {

    void Shoot(float speed);
    void ShootTargeted(float speed, Vector3 target);
    void ShootAngle(float speed, float launchAngle);
}
