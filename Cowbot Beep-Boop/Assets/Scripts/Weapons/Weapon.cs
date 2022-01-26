using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cowbot_Beep_Boop.Data;

public abstract class Weapon : MonoBehaviour {
    // public GameObject prefab;
    public SpaceShip spaceShip { get; private set; }
    public ProjectileTypeEnum pool;
    public float damage;
    public float speed;
    public float range;
    public float fireRate;
    public float cooldown => 1/fireRate;
    protected float canFireAfter;
    public bool IsInRange(Vector2 position, Vector2 target)
        => (position - target).magnitude < range;
    public void AssignParentSpaceship(SpaceShip spaceShip)
        => this.spaceShip = spaceShip;
}