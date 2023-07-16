using UnityEngine;

public class SurveillanceCamera : MonoBehaviour
{
    public float fieldOfViewAngle = 90f; // �ngulo de abertura do campo de vis�o da c�mera
    public float detectionRange = 10f; // Alcance m�ximo de detec��o da c�mera
    public LayerMask playerLayer; // Camada do jogador
    public GameObject alarmObject; // Objeto de alarme para acionar quando o jogador � detectado

    private void Update()
    {
        // Lan�a um raycast na dire��o do jogador
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRange, playerLayer))
        {
            // Verifica se o jogador est� dentro do �ngulo de abertura do campo de vis�o
            Vector3 directionToPlayer = hit.transform.position - transform.position;
            float angle = Vector3.Angle(directionToPlayer, transform.forward);

            if (angle <= fieldOfViewAngle * 0.5f)
            {
                // O jogador est� dentro do campo de vis�o da c�mera
                // Fa�a aqui as a��es que deseja quando o jogador � detectado
                Debug.Log("Jogador detectado!");

                // Ativa o objeto de alarme, se existir
                if (alarmObject != null)
                    alarmObject.SetActive(true);
            }
        }
        else
        {
            // O jogador n�o est� dentro do campo de vis�o da c�mera
            // Fa�a aqui as a��es que deseja quando o jogador n�o � detectado

            // Desativa o objeto de alarme, se existir
            if (alarmObject != null)
                alarmObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha o campo de vis�o da c�mera no editor do Unity
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -fieldOfViewAngle * 0.5f, 0) * transform.forward * detectionRange);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, fieldOfViewAngle * 0.5f, 0) * transform.forward * detectionRange);
        Gizmos.DrawRay(transform.position, transform.forward * detectionRange);

        Gizmos.DrawRay(transform.position, Quaternion.Euler(-fieldOfViewAngle * 0.3f, 0, 0) * transform.forward * detectionRange);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(fieldOfViewAngle * 0.3f, 0, 0) * transform.forward * detectionRange);
    }
}
