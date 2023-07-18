using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveillanceCamera : TrapCamera
{
    public float angle;
    public float rotationSpeed;
    public float waitTime;
    public bool isRotatingLeft;

    void Start()
    {
        StartCoroutine(RotateCamera());
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

    IEnumerator RotateCamera()
    {
        while (true)
        {
            // Verifica se a c�mera chegou no limite do �ngulo.
            //if (transform.eulerAngles.z >= angle || transform.eulerAngles.z <= -angle)
            //{
            //    // Se sim, muda a dire��o da rota��o.
            //    isRotatingLeft = !isRotatingLeft;
            //    //yield return null;
            //    yield return new WaitForSeconds(waitTime);
            //}

            // Rotaciona a c�mera para a esquerda ou direita, de acordo com o �ngulo configurado e se ela est� roteando para a esquerda ou para a direita.
            if (isRotatingLeft)
            {
                transform.Rotate(0, rotationSpeed, 0);
            }
            else
            {
                transform.Rotate(0, -rotationSpeed, 0);
            }

            // Espera por `waitTime` segundos antes de continuar.
        }
    }
}
