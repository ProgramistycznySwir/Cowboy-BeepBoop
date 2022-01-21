using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cowbot_Beep_Boop.Data;

namespace Cowbot_Beep_Boop.ProjectilePools
{
    // It would be better to dependancy inject it, but static will sufice
    public class ProjectilePools : MonoBehaviour
    {
        public GameObject[] prefabs;
        static ProjectilePools _instance;

        public ProjectilePools()
            => _instance = this;

        static Dictionary<ProjectileTypeEnum, ProjectilePool> pools = new();

        public static ProjectilePool GetPool(ProjectileTypeEnum type) {
            if(pools.TryGetValue(type, out ProjectilePool pool))
                return pool;

            Projectile prefab = _instance.prefabs
                    .Where(prefab => prefab.GetComponent<Projectile>().type == type)
                    .First()
                    .GetComponent<Projectile>();
            return pools[type] = new ProjectilePool(prefab);
        }
    }
}