using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimento do personagem
    public float rotationSpeed = 10f; // Velocidade de rota��o do personagem
    public ThirdPersonCamera cameraScript; // Refer�ncia ao script da c�mera em terceira pessoa

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Obt�m os valores de entrada horizontal e vertical
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcula o movimento com base na entrada e na rota��o da c�mera
        Vector3 cameraForward = Vector3.Scale(cameraScript.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = (verticalInput * cameraForward + horizontalInput * cameraScript.transform.right).normalized;

        // Calcula a rota��o com base na dire��o do movimento
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // Aplica o movimento ao Rigidbody do personagem
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
