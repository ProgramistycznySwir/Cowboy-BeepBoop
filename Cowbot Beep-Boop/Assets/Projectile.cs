using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cowbot_Beep_Boop.Data;
using Cowbot_Beep_Boop.ProjectilePools;

public abstract class Projectile : MonoBehaviour {
    public ProjectileTypeEnum type;
    public Color color;
    public int firedByID;
    public float lifeSpan;
    public float speed;
    public float damage;

    public void ReturnToPool()
    {
        ProjectilePools.GetPool(type).ReturnProjectile(this);
    }

    public void Move()
    {
        throw new NotImplementedException();
    } 

    public Projectile Clone()
    {
        // return new Projectile();
        throw new NotImplementedException(); 
    }
}