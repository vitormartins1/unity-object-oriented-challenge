using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveillanceCamera : TrapCamera
{
    void Start()
    {
        RotateCamera();
    }

    private void Update()
    {
        // Lan�a um raycast na dire��o do jogador
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRange, playerLayer))
        {
            PlayerIsInFieldOfView(hit);
        }
    }
}
