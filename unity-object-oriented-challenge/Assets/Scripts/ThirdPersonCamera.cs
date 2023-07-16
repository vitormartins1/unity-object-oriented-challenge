using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // O objeto que a câmera seguirá
    public float distance = 5f; // A distância da câmera em relação ao alvo
    public float height = 2f; // A altura da câmera em relação ao alvo
    public float rotationDamping = 3f; // A suavidade da rotação da câmera
    public float heightDamping = 2f; // A suavidade do movimento vertical da câmera

    private void LateUpdate()
    {
        // Verifica se o alvo está definido
        if (!target)
            return;

        // Calcula a rotação desejada com base na posição do alvo
        float desiredRotationAngle = target.eulerAngles.y;
        float desiredHeight = target.position.y + height;

        // Calcula a rotação e a altura atual da câmera
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Suaviza a rotação e a altura da câmera
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, desiredRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, desiredHeight, heightDamping * Time.deltaTime);

        // Converte o ângulo em rotação
        Quaternion currentRotation = Quaternion.Euler(0f, currentRotationAngle, 0f);

        // Define a posição da câmera
        Vector3 targetPosition = target.position - currentRotation * Vector3.forward * distance;
        targetPosition.y = currentHeight;

        // Atualiza a posição e a rotação da câmera
        transform.position = targetPosition;
        transform.LookAt(target);
    }
}
