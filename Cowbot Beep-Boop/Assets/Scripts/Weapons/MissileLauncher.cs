using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cowbot_Beep_Boop.ProjectilePools;

public class MissileLauncher : Weapon, ITurret {
    public float lockOnTime;
    Vector2 target;

    public void Fire()
    {
        if(canFireAfter < Time.time)
        {
            // TODO: Replace this targetting code when introducing this weapon to player.
            RaycastHit2D hit = Physics2D.CircleCast(target, 1f, Vector2.one, Single.Epsilon, 1<<LayerMask.NameToLayer("Player"));
            if(hit.collider is null)
                return;
            Projectile newRocket = ((HomingMissile)ProjectilePools.GetInstance().GetPool(pool).GetProjectile()).Init(
                firedByID: spaceShip.teamID,
                position: transform.position,
                target: hit.transform.GetComponent<SpaceShip>(),
                speed: speed,
                lifeSpan: range/speed,
                damage: damage
                // color: color,
            );
            canFireAfter = Time.time + cooldown;
        }
    }
    public bool AimAt(Vector2 target)
    {
        this.target = target;
        return true;
    }
}