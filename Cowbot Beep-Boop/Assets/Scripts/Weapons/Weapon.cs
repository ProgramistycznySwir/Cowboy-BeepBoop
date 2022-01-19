using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cowbot_Beep_Boop.Data;

public abstract class Weapon : MonoBehaviour {
    public GameObject prefab;
    public Transform barrelEnd; // Unity specyfic for placing projectiles in space.
    float damage;
    float speed;
    float range;
    public bool IsInRange(Vector2 position, Vector2 target)
        => (position - target).magnitude < range;
    ProjectileTypeEnum pool;
}