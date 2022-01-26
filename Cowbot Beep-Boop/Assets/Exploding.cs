using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cowbot_Beep_Boop.Data;

public class Exploding : Projectile{
    float explosionRadius;
    float proximityRadius;

    Vector2 velocity;

    public Projectile Init(int firedByID, Vector2 position, Vector2 velocity, float explosionRadius, float proximityRadius, float lifeSpan, float damage)
    {
        type = ProjectileTypeEnum.Exploding;
        this.velocity = velocity;
        this.explosionRadius = explosionRadius;
        this.proximityRadius = proximityRadius;
        Init(firedByID, position, lifeSpan, damage, Color.white);
        return this;
    }

    // Used only in FixedUpdate() context.
    protected override void Move()
    {
        if(Time.time > aliveUntil)
        {
            ReturnToPool();
            return;
        }
        Vector2 nextPosition = (Vector2)transform.position + velocity * Time.fixedDeltaTime;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, proximityRadius, nextPosition, (nextPosition-(Vector2)transform.position).magnitude);
        if(hits is not null)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                var hit = hits[i];
                var spaceship = hit.transform.GetComponentInParent<SpaceShip>();
                if(spaceship is not null && spaceship.teamID != firedByID)
                {
                    RaycastHit2D[] explosion = Physics2D.CircleCastAll(transform.position, explosionRadius, nextPosition, 0f);
                    if(explosion is not null)
                        foreach(var target in explosion)
                        {
                            target.transform.GetComponentInParent<SpaceShip>()?.ReceiveDamage(damage);
                        }
                    // spaceship.ReceiveDamage(damage);
                    ReturnToPool();
                    return;
                }
            }
        }
        transform.position = nextPosition;
        transform.up = velocity;
    }
}