using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cowbot_Beep_Boop.ProjectilePools;

public class MachineGun : Weapon, ITurret {
    float canFireAfter;
    public void Fire()
    {
        if(canFireAfter < Time.time)
        {
            Projectile newProjectile = ProjectilePools.GetPool(pool).GetProjectile().Init(
                firedByID: spaceShip.teamID,
                position: barrelEnd.position,
                velocity: barrelEnd.up * speed,
                lifeSpan: range/speed,
                damage: damage
                // color: color,
            );
            canFireAfter = Time.time + cooldown;
        }
    }
    public bool AimAt(Vector2 target)
    {
        transform.up = target - (Vector2)transform.position;

        return IsInRange(transform.position, target);
    }
}