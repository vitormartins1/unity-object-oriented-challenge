using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCamera : MonoBehaviour
{
    // all trap cameras catch the player and them gameover

    public float fieldOfViewAngle = 90f; // Ângulo de abertura do campo de visão da câmera
    public float detectionRange = 10f; // Alcance máximo de detecção da câmera
    public LayerMask playerLayer; // Camada do jogador

    public virtual void PlayerIsInFieldOfView(RaycastHit hit) 
    {
        Vector3 directionToPlayer = hit.transform.position - transform.position;
        float angle = Vector3.Angle(directionToPlayer, transform.forward);

        if (angle <= fieldOfViewAngle * 0.5f)
        {
            Debug.Log("Jogador detectado!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha o campo de visão da câmera no editor do Unity
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -fieldOfViewAngle * 0.5f, 0) * transform.forward * detectionRange);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, fieldOfViewAngle * 0.5f, 0) * transform.forward * detectionRange);
        Gizmos.DrawRay(transform.position, transform.forward * detectionRange);

        Gizmos.DrawRay(transform.position, Quaternion.Euler(-fieldOfViewAngle * 0.3f, 0, 0) * transform.forward * detectionRange);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(fieldOfViewAngle * 0.3f, 0, 0) * transform.forward * detectionRange);
    }
}