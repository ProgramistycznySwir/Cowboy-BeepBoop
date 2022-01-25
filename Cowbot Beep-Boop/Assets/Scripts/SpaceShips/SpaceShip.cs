using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cowbot_Beep_Boop.Data;
// using System.Reactive.Subjects;

// It's basically an abstract class, but Unity prevents
public abstract class SpaceShip : MonoBehaviour
{
    public int teamID { get; protected set; }
    public Rigidbody2D rigidbody;

    public float turnRate;
    public ITurret weaponControlSystem;
    public Transform weaponControlSystem_transform;
    public float health_max;
    protected MyBehaviourSubject<float> health;
    public float speed;

    protected void Awake()
    {
        health = new(health_max);
        weaponControlSystem = weaponControlSystem_transform.GetComponent<ITurret>();
        AssignParentToAllWeapons(weaponControlSystem_transform);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Use this method in Update context.
    /// </summary>
    public void Move(float throttle, float steering)
    {
        if(Time.timeScale is 0f)
            return;
        rigidbody.AddRelativeForce(new Vector2(0, throttle * speed));
        rigidbody.MoveRotation(rigidbody.rotation + steering * turnRate * Time.deltaTime);
    }
    public void AimAt(Vector2 target)
    {
        weaponControlSystem.AimAt(target);
    }
    public void Fire()
    {
        weaponControlSystem.Fire();
    }

    public void ReceiveDamage(float dmg)
    {
        health.Value -= dmg;
        if(health.Value <= 0)
        {
            OnDeath();
        }
    }
    public IDisposable SubscribeToHealthChange(Action<float> next)
    {
        return health.SubscribeOnce(next);
    }

    public void AssignParentToAllWeapons(Transform root)
    {
        foreach (Weapon weapon in root.GetComponentsInChildren<Weapon>())
            weapon.AssignParentSpaceship(this);
    }

    protected abstract void OnDeath();
}
