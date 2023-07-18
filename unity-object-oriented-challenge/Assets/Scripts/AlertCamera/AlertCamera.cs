using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertCamera : TrapCamera
{


    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRange, playerLayer))
        {
            PlayerIsInFieldOfView(hit);
        }
    }
}
