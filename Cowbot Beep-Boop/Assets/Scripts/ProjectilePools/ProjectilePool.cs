using System;
using System.Collections.Generic;

namespace Cowbot_Beep_Boop.ProjectilePools
{
    public class ProjectilePool
    {
        Projectile prefab;
        Stack<Projectile> pool;

        public Projectile GetProjectile() {
            if(pool.Count > 0)
                return pool.Pop();
            return prefab.Clone();
        }

        public void  ReturnProjectile(Projectile projectile) {
            pool.Push(projectile);
        }
    }
}