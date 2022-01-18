using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpaceShip : SpaceShip
{
    public int upgradeLvl;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RegisterInput();
    }

    public void RegisterInput()
    {
        float throttle = Input.GetAxis("Vertical");
        float steering = -Input.GetAxis("Horizontal");

        Move(throttle, steering);
    }
}
