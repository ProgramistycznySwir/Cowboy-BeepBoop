using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurret {
    public Projectile Fire();
    public bool AimAt(Vector2 target);
}