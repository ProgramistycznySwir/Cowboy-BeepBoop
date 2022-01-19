using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ITurret {
    public Projectile Fire();
    public bool AimAt(Vector2 position);
}