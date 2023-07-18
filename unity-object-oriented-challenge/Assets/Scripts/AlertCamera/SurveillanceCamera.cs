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
        // Lança um raycast na direção do jogador
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRange, playerLayer))
        {
            PlayerIsInFieldOfView(hit);
        }
    }
}
