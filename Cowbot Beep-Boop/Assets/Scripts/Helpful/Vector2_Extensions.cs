using System;
using UnityEngine;

namespace Cowbot_Beep_Boop.Helpful
{
    public class Vector2_Extensions
    {
        public static Vector2 FromAngle(float angle)
        {
            float angle_rad = angle * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(angle_rad), Mathf.Sin(angle_rad));
        }
        public static Vector2 FromRandomAngle()
        {
            System.Random rng = new();
            float angle = (float)rng.NextDouble() * 360f;
            float angle_rad = angle * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(angle_rad), Mathf.Sin(angle_rad));
        }
    }
}