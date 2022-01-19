using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cowbot_Beep_Boop.Data;
// using System.Reactive.Subjects;

public class SpaceShip : MonoBehaviour
{
    public Rigidbody2D rigidbody;

    public float turnRate;
    // public ITurret[] turrets;
    public float health_max;
    public MyBehaviourSubject<float> health;
    public float speed;

    void Awake()
    {
        health = new(health_max);
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
        rigidbody.AddRelativeForce(new Vector2(0, throttle * speed));
        rigidbody.MoveRotation(rigidbody.rotation + steering * turnRate * Time.deltaTime);
    }

    public void ReceiveDamage(float dmg)
    {
        health.Value -= dmg;
    }
    public IDisposable SubscribeToHealthChange(Action<float> action)
    {
        return health.SubscribeOnce(action);
    }
}
