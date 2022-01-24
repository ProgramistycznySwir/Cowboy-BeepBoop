using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotRotating : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
