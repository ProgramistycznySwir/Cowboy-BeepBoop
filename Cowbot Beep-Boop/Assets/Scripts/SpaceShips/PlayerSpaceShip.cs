using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpaceShip : SpaceShip
{
    static PlayerSpaceShip _instance;

    public int upgradeLvl;

    public PlayerSpaceShip() {
        _instance = this;
        teamID = 0;
    }

    void Awake()
    {
        base.Awake();
        playerTransform = this.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        SubscribeToHealthChange(next: (param) => Debug.Log(param));
        
    }

    // Update is called once per frame
    void Update()
    {
        var(throttle, steering, aimAt, fire) = RegisterInput();
        Move(throttle, steering);
        AimAt(aimAt);
        if(fire)
            Fire();
    }

    public (float throttle, float steering, Vector2 aimAt, bool fire) RegisterInput()
    {
        float throttle = Input.GetAxis("Vertical");
        float steering = -Input.GetAxis("Horizontal");
        Vector2 aimAt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool fire = Input.GetAxis("Fire1") is not 0;

        return (throttle, steering, aimAt, fire);
    }

    private static Transform playerTransform;
    public static Vector2 GetPosition()
        => playerTransform.position;
    public static PlayerSpaceShip GetPlayer()
        => _instance;
}