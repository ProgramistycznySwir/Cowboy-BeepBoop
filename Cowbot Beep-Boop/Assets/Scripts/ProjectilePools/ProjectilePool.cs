using System;
using System.Collections.Generic;

namespace Cowbot_Beep_Boop.ProjectilePools
{
    public class ProjectilePool
    {
        Projectile prototype;
        Stack<Projectile> pool = new();

        public ProjectilePool(Projectile prototype)
        {
            this.prototype = prototype;
        }

        public Projectile GetProjectile() {
            if(pool.Count > 0)
                return pool.Pop();
            return prototype.Clone();
        }

        public void ReturnProjectile(Projectile projectile) {
            pool.Push(projectile);
        }
    }
}