using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurret {
    public void Fire();
    public bool AimAt(Vector2 target);
}