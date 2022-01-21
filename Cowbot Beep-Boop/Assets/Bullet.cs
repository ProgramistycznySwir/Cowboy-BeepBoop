using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile {
    // Used only in FixedUpdate() context.
    protected override void Move()
    {
        if(Time.time > aliveUntil)
        {
            ReturnToPool();
            return;
        }
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