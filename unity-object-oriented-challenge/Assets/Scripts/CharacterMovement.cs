using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour
{
    public float rotationSpeed = 10f; // Velocidade de rota��o do personagem
    public ThirdPersonCamera cameraScript; // Refer�ncia ao script da c�mera em terceira pessoa
    public Animator animator; // Refer�ncia ao componente Animator do personagem

    private NavMeshAgent agent;
    private OffMeshLink currentLink; // OffMeshLink atual
    [SerializeField] private bool isClimbing; // Indicador se o personagem est� subindo uma escada

    private Vector3 startPosition; // Posi��o inicial do OffMeshLink
    private Vector3 endPosition; // Posi��o final do OffMeshLink
    private Vector3 targetPosition;

    private float climbTime; // Tempo de subida da escada
    public float climbSpeed = 3f; // Velocidade de subida da escada
    private float currentMoveSpeed = 0f; // Velocidade de movimento atual do personagem
    [SerializeField] private float moveSpeedDamping = 0.1f; // Amortecimento da velocidade de movimento

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // Desabilita a rota��o autom�tica do NavMesh Agent
    }

    private void Update()
    {
        // Verifica se o personagem est� subindo a escada
        if (isClimbing)
        {
            Vector3 desiredDirection = targetPosition - transform.position;

            // Atualiza a posi��o do personagem em dire��o ao destino
            //transform.position = Vector3.Lerp(transform.position, targetPosition, agent.speed * Time.deltaTime);

            climbTime += Time.deltaTime;
            float t = climbTime / climbSpeed;
            transform.position = Vector3.Lerp(transform.position, targetPosition, t);

            // Verifica se o personagem chegou ao destino do OffMeshLink
            if (Vector3.Distance(transform.position, targetPosition) <= agent.stoppingDistance)
            {
                // Finaliza a subida ou descida da escada
                FinishClimbing();
            }
        }
        else
        {
            // Obt�m os valores de entrada horizontal e vertical
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calcula o movimento com base na entrada e na rota��o da c�mera
            Vector3 cameraForward = Vector3.Scale(cameraScript.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 movement = (verticalInput * cameraForward + horizontalInput * cameraScript.transform.right).normalized;

            // Calcula a dire��o do movimento
            Vector3 desiredDirection = Vector3.zero;
            if (movement.magnitude >= 0.1f)
                desiredDirection = movement;

            // Rotaciona o personagem na dire��o do movimento
            if (desiredDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(desiredDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }

            // Define a velocidade do NavMesh Agent
            agent.velocity = desiredDirection * agent.speed;

            // Interpola a velocidade de movimento atual
            currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, movement.magnitude, moveSpeedDamping);

            // Atualiza os par�metros do Animator
            animator.SetFloat("MoveSpeed", currentMoveSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o personagem encontrou um OffMeshLink
        OffMeshLink offMeshLink = other.GetComponent<OffMeshLink>();
        if (offMeshLink != null && offMeshLink.activated && !isClimbing)
        {
            // Inicia a subida ou descida da escada
            StartClimbing(offMeshLink);
        }
    }

    private void StartClimbing(OffMeshLink offMeshLink)
    {
        // Desabilita o NavMeshAgent durante a subida ou descida da escada
        agent.enabled = false;

        // Atualiza o OffMeshLink atual
        currentLink = offMeshLink;

        // Define a posi��o inicial e final do OffMeshLink
        startPosition = offMeshLink.startTransform.position;
        endPosition = offMeshLink.endTransform.position;

        // Calcula a dire��o e o destino com base na posi��o mais pr�xima (startPosition ou endPosition)
        targetPosition = Vector3.Distance(transform.position, startPosition) < Vector3.Distance(transform.position, endPosition) ? endPosition : startPosition;

        // Define a flag de subida ou descida da escada
        isClimbing = true;
    }

    private void FinishClimbing()
    {
        // Habilita o NavMeshAgent ap�s a subida da escada
        agent.enabled = true;
        climbTime = 0;
        // Reseta o OffMeshLink atual e a flag de subida da escada
        currentLink = null;
        isClimbing = false;
    }
}
