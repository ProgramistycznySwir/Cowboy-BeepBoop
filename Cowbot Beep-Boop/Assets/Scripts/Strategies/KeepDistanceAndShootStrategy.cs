using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cowbot_Beep_Boop.Strategies
{
    public class KeepDistanceAndShootStrategy : IStrategy
    {

        public Color GetColor() {
            Debug.LogError("Not implemented!");
            return Color.white;
        }

        public Vector2 Fire(Vector2 position, Vector2 playerPosition) {
            return playerPosition;
        }

        public (float throttle, float steering) Move(Vector2 position, Vector2 forward, Vector2 playerPosition) {
            float throttle = 0;
            float steering = 0;

            return (throttle, steering);
        }
    }
}
