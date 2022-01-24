using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cowbot_Beep_Boop.Strategies
{
    public class KeepDistanceStrategy : IStrategy
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

            Vector2 playerDelta = playerPosition - position;
            float playerAngle = Vector2.SignedAngle(forward, playerDelta);

            // if(Mathf.Abs(playerAngle) < angleThreshold)
            //     steering = 0;
            // else
            //     steering = playerAngle > 0 ? 1 : -1;
            
            // throttle = 1;

            return (throttle, steering);
        }
    }
}
