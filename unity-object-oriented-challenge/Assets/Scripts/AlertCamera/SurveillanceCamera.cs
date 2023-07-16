using UnityEngine;

public class SurveillanceCamera : MonoBehaviour
{
    public float fieldOfViewAngle = 90f; // Ângulo de abertura do campo de visão da câmera
    public float detectionRange = 10f; // Alcance máximo de detecção da câmera
    public LayerMask playerLayer; // Camada do jogador
    public GameObject alarmObject; // Objeto de alarme para acionar quando o jogador é detectado

    private void Update()
    {
        // Lança um raycast na direção do jogador
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRange, playerLayer))
        {
            // Verifica se o jogador está dentro do ângulo de abertura do campo de visão
            Vector3 directionToPlayer = hit.transform.position - transform.position;
            float angle = Vector3.Angle(directionToPlayer, transform.forward);

            if (angle <= fieldOfViewAngle * 0.5f)
            {
                // O jogador está dentro do campo de visão da câmera
                // Faça aqui as ações que deseja quando o jogador é detectado
                Debug.Log("Jogador detectado!");

                // Ativa o objeto de alarme, se existir
                if (alarmObject != null)
                    alarmObject.SetActive(true);
            }
        }
        else
        {
            // O jogador não está dentro do campo de visão da câmera
            // Faça aqui as ações que deseja quando o jogador não é detectado

            // Desativa o objeto de alarme, se existir
            if (alarmObject != null)
                alarmObject.SetActive(false);
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
