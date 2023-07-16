using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // O objeto que a c�mera seguir�
    public float distance = 5f; // A dist�ncia da c�mera em rela��o ao alvo
    public float height = 2f; // A altura da c�mera em rela��o ao alvo
    public float rotationDamping = 3f; // A suavidade da rota��o da c�mera
    public float heightDamping = 2f; // A suavidade do movimento vertical da c�mera

    private void LateUpdate()
    {
        // Verifica se o alvo est� definido
        if (!target)
            return;

        // Calcula a rota��o desejada com base na posi��o do alvo
        float desiredRotationAngle = target.eulerAngles.y;
        float desiredHeight = target.position.y + height;

        // Calcula a rota��o e a altura atual da c�mera
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Suaviza a rota��o e a altura da c�mera
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, desiredRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, desiredHeight, heightDamping * Time.deltaTime);

        // Converte o �ngulo em rota��o
        Quaternion currentRotation = Quaternion.Euler(0f, currentRotationAngle, 0f);

        // Define a posi��o da c�mera
        Vector3 targetPosition = target.position - currentRotation * Vector3.forward * distance;
        targetPosition.y = currentHeight;

        // Atualiza a posi��o e a rota��o da c�mera
        transform.position = targetPosition;
        transform.LookAt(target);
    }
}
