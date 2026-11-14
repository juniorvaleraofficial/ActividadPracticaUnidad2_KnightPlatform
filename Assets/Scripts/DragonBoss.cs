
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class DragonBoss : MonoBehaviour
{
    [Header("Detección del jugador")]
    [SerializeField] private float detectionRange = 6f;

    [Header("Movimiento del dragón")]
    [SerializeField] private float flyHeight = 1.5f;
    [SerializeField] private float flySpeed = 2f;
    [SerializeField] private float chaseSpeed = 2.5f;
    [SerializeField] private float stoppingDistance = 1.8f;

    [Header("Ataque de fuego")]
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private float attackRange = 2.8f;
    [SerializeField] private float attackHeight = 2.5f;

    private Transform player;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector3 initialPosition;
    private float nextAttackTime;
    private bool isFlying;
    private bool isRestarting;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        initialPosition = transform.position;

        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning(
                "DragonBoss: no se encontró al jugador."
            );
        }
    }

    void Update()
    {
        if (player == null || isRestarting)
        {
            return;
        }

        // Calcula la distancia horizontal desde
        // la posición inicial del dragón.
        float detectionDistance = Mathf.Abs(
            player.position.x - initialPosition.x
        );

        isFlying = detectionDistance <= detectionRange;

        animator.SetBool("IsFlying", isFlying);

        if (isFlying)
        {
            ChasePlayer();
            TryAttack();
        }
        else
        {
            ReturnToStart();
        }
    }


    private void ChasePlayer()
    {
        // Calcula hacia dónde se encuentra el jugador.
        float differenceX =
            player.position.x - transform.position.x;

        // Orienta al dragón hacia el jugador.
        spriteRenderer.flipX = differenceX < 0f;

        // Mantiene cierta distancia horizontal
        // para poder atacar desde el frente.
        float targetX = transform.position.x;

        if (Mathf.Abs(differenceX) > stoppingDistance)
        {
            targetX = player.position.x -
                Mathf.Sign(differenceX) * stoppingDistance;
        }

        // El dragón baja o sube según la altura
        // del jugador, sin bajar del suelo.
        float targetY = Mathf.Max(
            initialPosition.y,
            player.position.y + 0.7f
        );

        // Se desplaza hacia el jugador.
        float newX = Mathf.MoveTowards(
            transform.position.x,
            targetX,
            chaseSpeed * Time.deltaTime
        );

        float newY = Mathf.MoveTowards(
            transform.position.y,
            targetY,
            flySpeed * Time.deltaTime
        );

        transform.position = new Vector3(
            newX,
            newY,
            initialPosition.z
        );
    }


    private void TryAttack()
    {
        // Respeta el tiempo de espera.
        if (Time.time < nextAttackTime)
        {
            return;
        }

        float distanceX = Mathf.Abs(
            player.position.x - transform.position.x
        );

        float distanceY = Mathf.Abs(
            player.position.y - transform.position.y
        );

        // Solo ataca cuando tiene al jugador
        // suficientemente cerca.
        if (distanceX <= attackRange &&
            distanceY <= attackHeight)
        {
            animator.SetTrigger("FireAttack");

            nextAttackTime =
                Time.time + attackCooldown;
        }
    }

    private void ReturnToStart()
    {
        // Regresa a su posición original.
        transform.position = Vector3.MoveTowards(
            transform.position,
            initialPosition,
            flySpeed * Time.deltaTime
        );
    }

    // Este método lo ejecuta el Animation Event
    // configurado en Dragon_FireAttack.
    public void ApplyFireDamage()
    {
        if (player == null || isRestarting)
        {
            return;
        }

        float distanceX = Mathf.Abs(
            player.position.x - transform.position.x
        );

        float distanceY = Mathf.Abs(
            player.position.y - transform.position.y
        );

        float differenceX =
            player.position.x - transform.position.x;

        bool playerInFront = spriteRenderer.flipX
            ? differenceX <= 0f
            : differenceX >= 0f;

        // Comprueba que el jugador esté dentro
        // del alcance del fuego y frente al dragón.
        if (playerInFront &&
            distanceX <= attackRange &&
            distanceY <= attackHeight)
        {
            isRestarting = true;

            Debug.Log(
                "DragonBoss: el fuego alcanzó al jugador."
            );

            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }
    }
}
