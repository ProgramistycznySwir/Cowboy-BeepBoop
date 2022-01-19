using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : Weapon, ITurret {
    public float lockOnTime;

    public Projectile Fire()
    {
        throw new NotImplementedException();
    }
    public bool AimAt(Vector2 target)
    {
        throw new NotImplementedException();
    }
}