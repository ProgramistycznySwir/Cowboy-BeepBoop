using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStrategy
{
    Color GetColor();
    Vector2 Fire(Vector2 position, Vector2 playerPosition);
    (float throttle, float steering) Move(Vector2 position, Vector2 playerPosition);
}
