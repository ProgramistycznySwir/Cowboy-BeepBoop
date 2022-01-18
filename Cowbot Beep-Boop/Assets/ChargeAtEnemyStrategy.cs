using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAtEnemyStrategy : IStrategy
{
    public Color GetColor() {
        return Color.red;
    }

    public Vector2 Fire(Vector2 position, Vector2 playerPosition) {
        return Vector2.zero;
    }

    public (float throttle, float steering) Move(Vector2 position, Vector2 playerPosition) {
        float throttle = 0;
        float steering = 0;

        return (throttle, steering);
    }
}