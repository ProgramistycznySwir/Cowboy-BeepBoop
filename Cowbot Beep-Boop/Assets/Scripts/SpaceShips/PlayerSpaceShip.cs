using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpaceShip : SpaceShip
{
    static PlayerSpaceShip _instance;

    public int upgradeLvl;

    public SpriteRenderer engineExhaust_Animation;

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
        // SubscribeToHealthChange(next: (param) => Debug.Log(param));
        
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

    #region >>> Upgrades <<<
    public void UpgradeHP()
    {
        health_max *= 1.25f;
        health.OnNext();
    }
    public void Heal()
    {
        health.Value = health_max;
    }
    public void UpgradeDmg()
    {
        weaponControlSystem_transform.GetComponentInChildren<Weapon>().damage *= 1.5f;
    }
    public void UpgradeRoF()
    {
        weaponControlSystem_transform.GetComponentInChildren<Weapon>().fireRate *= 1.5f;
    }
    #endregion

    protected override void OnDeath()
    {
        // EnemyManager.GetInstance().RemoveEnemy(this);
        // TODO: Trigger GameOver logic
        // Destroy(gameObject);
    }
}
