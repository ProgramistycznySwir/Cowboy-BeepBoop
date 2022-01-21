using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile {
    // Used only in FixedUpdate() context.
    protected override void Move()
    {
        Vector2 nextPosition = (Vector2)transform.position + velocity * Time.fixedDeltaTime;
        RaycastHit2D hit = Physics2D.Linecast(transform.position, nextPosition);
        if(hit.collider is not null)
        {
            hit.transform.GetComponentInParent<SpaceShip>().ReceiveDamage(damage);
            ReturnToPool();
            return;
        }
        transform.position = nextPosition;
        transform.up = velocity;
    }
}