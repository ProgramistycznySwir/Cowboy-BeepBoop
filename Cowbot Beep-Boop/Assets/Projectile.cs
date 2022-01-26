using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cowbot_Beep_Boop.Data;
using Cowbot_Beep_Boop.ProjectilePools;

public abstract class Projectile : MonoBehaviour {
    public ProjectileTypeEnum type;
    public int firedByID;
    public Color color;
    public float lifeSpan;
    protected float aliveUntil;
    public float damage;

    public Projectile Init(int firedByID, Vector2 position, float lifeSpan, float damage)
    {
        Init(firedByID, position, lifeSpan, damage, Color.white);
        return this;
    }
    public Projectile Init(int firedByID, Vector2 position, float lifeSpan, float damage, Color color)
    {
        gameObject.SetActive(true);
        transform.position = position;
        // this.velocity = velocity;
        this.firedByID = firedByID;
        this.lifeSpan = lifeSpan;
        aliveUntil = Time.time + lifeSpan;
        this.damage = damage;
        this.color = color;
        return this;
    }

    void FixedUpdate()
    {
        Move();
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        ProjectilePools.GetInstance().GetPool(type).ReturnProjectile(this);
    }

    protected abstract void Move();

    public Projectile Clone()
    {
        GameObject newProjectileGO = GameObject.Instantiate(gameObject);
        newProjectileGO.SetActive(false);
        return newProjectileGO.GetComponent<Projectile>();
        // throw new NotImplementedException(); 
    }
}