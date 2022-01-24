using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cowbot_Beep_Boop.Helpful;

public class WeaponGroup : MonoBehaviour, ITurret {
    //IWeapon subWeapons;

    ITurret[] subWeapons = new ITurret[0];

    public void Start()
    {
        ReMapWeapons();
    }

    public void Fire()
    {
        foreach(var subWeapon in subWeapons)
            subWeapon.Fire();
    }
    public bool AimAt(Vector2 target)
    {
        // Debug.Log("sPain");
        // Debug.Log(subWeapons.Length);
        // if(subWeapons[0] == this)
        //     Debug.Log("TAK.!!");
        for(int i = 0; i < subWeapons.Length; i++)
            subWeapons[i].AimAt(target);
        return true;
    }

    public void ReMapWeapons()
    {
        subWeapons = transform.GetComponentsInChildrenWithDepthOne<ITurret>();
        Debug.Log(subWeapons.Length);
    }
}