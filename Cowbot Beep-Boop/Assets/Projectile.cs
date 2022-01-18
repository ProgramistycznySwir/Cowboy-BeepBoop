using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cowbot_Beep_Boop.Data;
using Cowbot_Beep_Boop.ProjectilePools;

public abstract class Projectile {
    public ProjectileTypeEnum type { get; }
    public Color color { get; }
    public int firedByID { get; }
    public float lifeSpan { get; }
    public float speed { get; }

    public void ReturnToPool()
    {
        ProjectilePools.GetPool(type).ReturnProjectile(this);
    }

    public void Move()
    {
        throw new NotImplementedException();
    } 

    public Projectile Clone() {

        // return new Projectile();
        throw new NotImplementedException(); 
    }
}