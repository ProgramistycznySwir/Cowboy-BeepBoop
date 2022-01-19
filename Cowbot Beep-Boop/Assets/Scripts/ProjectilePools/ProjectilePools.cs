using System;
using System.Collections.Generic;
using Cowbot_Beep_Boop.Data;

namespace Cowbot_Beep_Boop.ProjectilePools
{
    // It would be better to dependancy inject it, but static will sufice
    public static class ProjectilePools
    {
        static Dictionary<ProjectileTypeEnum, ProjectilePool> pools = new();

        public static ProjectilePool GetPool(ProjectileTypeEnum type) {
            if(pools.TryGetValue(type, out ProjectilePool pool))
                return pool;
            return pools[type] = new ProjectilePool();
        }
    }
}