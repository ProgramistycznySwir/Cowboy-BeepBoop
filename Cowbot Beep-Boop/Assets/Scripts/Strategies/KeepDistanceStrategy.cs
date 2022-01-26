using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cowbot_Beep_Boop.Strategies
{
    public class KeepDistanceStrategy : IStrategy
    {
        private readonly float angleThreshold;
        private readonly float keepDistance;
        private readonly float keepDistanceThreshold;

        public KeepDistanceStrategy(float angleThreshold = 5f, float keepDistance = 10f, float keepDistanceThreshold = 1f)
        {
            this.angleThreshold = angleThreshold;
            this.keepDistance = keepDistance;
            this.keepDistanceThreshold = keepDistanceThreshold;
        }

        public Color GetColor() {
            Debug.LogError("Not implemented!");
            return Color.white;
        }

        public Vector2 Fire(Vector2 position, Vector2 playerPosition) {
            // TODO: Make target predictions.
            return playerPosition;
        }

        public (float throttle, float steering) Move(Vector2 position, Vector2 forward, Vector2 playerPosition) {
            float throttle = 0;
            float steering = 0;

            Vector2 playerDelta = playerPosition - position;
            float playerAngle = Vector2.SignedAngle(forward, playerDelta);

            if(Mathf.Abs(playerAngle) < angleThreshold)
                steering = 0;
            else
                steering = playerAngle > 0 ? 1 : -1;
            
            // throttle = 0.2f + 0.8f * Mathf.Lerp(0, 1, flightTowardsDelta.magnitude/orbitRadius);
            float distance = (playerPosition - position).magnitude;
            if(distance > (keepDistance + keepDistanceThreshold))
                throttle = 1f;
            else if(distance < (keepDistance - keepDistanceThreshold))
                throttle = -1f;

            return (throttle, steering);
        }
    }
}
