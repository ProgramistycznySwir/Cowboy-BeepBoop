using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cowbot_Beep_Boop.Data;

public class HomingMissile : Projectile {
    // float turnRate;
    SpaceShip target;
    float speed;
    // public bool predict;

    public Projectile Init(int firedByID, Vector2 position, SpaceShip target, float speed, float lifeSpan, float damage)
    {
        type = ProjectileTypeEnum.HomingMissile;
        this.target = target;
        this.speed = speed;
        Init(firedByID, position, lifeSpan, damage, Color.white);
        return this;
    }

    protected override void Move()
    {
        if(Time.time > aliveUntil)
        {
            ReturnToPool();
            return;
        }
        Vector2 velocity = (target.transform.position - transform.position).normalized * speed;
        Vector2 nextPosition = (Vector2)transform.position + velocity * Time.fixedDeltaTime;
        RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, nextPosition);
        if(hits is not null)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                var hit = hits[i];
                var spaceship = hit.transform.GetComponentInParent<SpaceShip>();
                if(spaceship is not null && spaceship.teamID != firedByID)
                {
                    spaceship.ReceiveDamage(damage);
                    ReturnToPool();
                    return;
                }
            }
        }
        transform.position = nextPosition;
        transform.up = velocity;
    }
}