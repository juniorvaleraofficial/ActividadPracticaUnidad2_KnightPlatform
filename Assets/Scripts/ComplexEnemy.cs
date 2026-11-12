
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class ComplexEnemy : MonoBehaviour
{
    [Header("Patrullaje")]
    [SerializeField] private float patrolSpeed = 1.5f;
    [SerializeField] private float patrolDistance = 1.5f;
    [SerializeField] private float stopDuration = 1f;

    [Header("Persecución")]
    [SerializeField] private float chaseSpeed = 3.5f;
    [SerializeField] private float detectionRange = 4f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform player;

    private float leftLimit;
    private float rightLimit;
    private float stopTimer;

    private int direction = 1;
    private bool isWaiting = false;

    // Inicializa los componentes y encuentra al jugador.
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        // Define los límites desde la posición inicial.
        leftLimit = rb.position.x - patrolDistance;
        rightLimit = rb.position.x + patrolDistance;
    }

    // Actualiza el comportamiento mediante las físicas 2D.
    private void FixedUpdate()
    {
        if (player != null &&
            Vector2.Distance(rb.position, player.position)
                <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    // Persigue horizontalmente al jugador.
    private void ChasePlayer()
    {
        float difference = player.position.x - rb.position.x;

        // Evita cambios bruscos si están casi alineados.
        if (Mathf.Abs(difference) < 0.2f)
        {
            SetHorizontalVelocity(0f);
            animator.SetInteger("State", 0);
            return;
        }

        direction = difference > 0 ? 1 : -1;

        SetHorizontalVelocity(direction * chaseSpeed);

        animator.SetInteger("State", 2);
        UpdateDirection();
    }

    // Patrulla dentro de los límites establecidos.
    private void Patrol()
    {
        if (isWaiting)
        {
            stopTimer -= Time.fixedDeltaTime;

            SetHorizontalVelocity(0f);
            animator.SetInteger("State", 0);

            if (stopTimer <= 0f)
            {
                isWaiting = false;
            }

            return;
        }

        float currentX = rb.position.x;

        if ((direction == 1 && currentX >= rightLimit) ||
            (direction == -1 && currentX <= leftLimit))
        {
            direction *= -1;
            isWaiting = true;
            stopTimer = stopDuration;

            SetHorizontalVelocity(0f);
            animator.SetInteger("State", 0);
            return;
        }

        SetHorizontalVelocity(direction * patrolSpeed);

        animator.SetInteger("State", 1);
        UpdateDirection();
    }

    // Modifica solo el movimiento horizontal.
    private void SetHorizontalVelocity(float speed)
    {
        rb.linearVelocity = new Vector2(
            speed,
            rb.linearVelocity.y
        );
    }

    // Gira el sprite hacia la dirección de movimiento.
    private void UpdateDirection()
    {
        spriteRenderer.flipX = direction < 0;
    }
}
