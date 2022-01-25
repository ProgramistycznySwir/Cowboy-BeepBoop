using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cowbot_Beep_Boop.Strategies;

public class EnemySpaceShip : SpaceShip
{
    public StrategyType strategyType;
    public IStrategy strategy { get; private set; }
    // For sake of auto-generating levels
    public int difficulty;

    public EnemySpaceShip() {
        teamID = 1;
    }
    public EnemySpaceShip Init(IStrategy strategy)
    {
        this.strategy = strategy;
        return this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(strategy is null)
            // Init(new ChargeAtEnemyStrategy());
            Init(IStrategy.GetStrategy(strategyType));
    }

    // Update is called once per frame
    void Update()
    {
        PerformMove();
        PerformFire();
    }

    public void PerformMove()
    {
        var(throttle, steering) = strategy.Move(transform.position, transform.up, PlayerSpaceShip.GetPosition());

        Move(throttle, steering);
    }

    public void PerformFire()
    {
        // TODO: Change it to rely on IStrategy.
        AimAt(PlayerSpaceShip.GetPosition());
        weaponControlSystem.Fire();
    }
}
