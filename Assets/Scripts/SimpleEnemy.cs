
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    [Header("Configuración del movimiento")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float patrolDistance = 2f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float leftLimit;
    private float rightLimit;
    private int direction = 1;

    // Inicializa los componentes y los límites del patrullaje.
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Los límites se calculan desde la posición inicial.
        leftLimit = transform.position.x - patrolDistance;
        rightLimit = transform.position.x + patrolDistance;
    }

    // Controla el desplazamiento utilizando las físicas 2D.
    private void FixedUpdate()
    {
        float currentX = rb.position.x;

        // Cambia de dirección al alcanzar un límite.
        if (direction == 1 && currentX >= rightLimit)
        {
            direction = -1;
        }
        else if (direction == -1 && currentX <= leftLimit)
        {
            direction = 1;
        }

        // Mantiene la velocidad vertical para conservar la gravedad.
        rb.linearVelocity = new Vector2(
            direction * moveSpeed,
            rb.linearVelocity.y
        );

        // Actualiza la animación según la velocidad horizontal.
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        // Orienta el sprite hacia la dirección del movimiento.
        spriteRenderer.flipX = direction < 0;
    }
}
