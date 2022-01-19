using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon, ITurret {

    public Projectile Fire()
    {
        throw new NotImplementedException();
    }
    public bool AimAt(Vector2 target)
    {
        transform.up = target - (Vector2)transform.position;

        return IsInRange(transform.position, target);
    }
}