using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cowbot_Beep_Boop.Helpful;

namespace Cowbot_Beep_Boop.Strategies
{
    public class DefaultStrategy : IStrategy
    {
        private readonly float angleThreshold;
        private readonly float orbitRadius;
        private readonly float orbitAngle = 0.5f;

        public DefaultStrategy(float angleThreshold = 5f, float orbitRadius = 2.5f)
        {
            this.angleThreshold = angleThreshold;
            this.orbitRadius = orbitRadius;
        }

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

            Vector2 enemyDelta = position - playerPosition;
            float enemyAngle = Vector2.SignedAngle(Vector2.up, enemyDelta);
            Vector2 flightTowards = playerPosition + Vector2_Extensions.FromAngle(enemyAngle + orbitAngle) * orbitRadius;
            Vector2 flightTowardsDelta = flightTowards - position;
            float flightTowardsAngle = Vector2.SignedAngle(forward, flightTowardsDelta);

            if(Mathf.Abs(flightTowardsAngle) < angleThreshold)
                steering = 0;
            else
                steering = flightTowardsAngle > 0 ? 1 : -1;
            
            throttle = 0.2f + 0.8f * Mathf.Lerp(0, 1, flightTowardsDelta.magnitude/orbitRadius);
            
            // Debug.Log(throttle, steering);
            return (throttle, steering);
        }
    }
}
