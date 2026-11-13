
using UnityEngine;
using UnityEngine.SceneManagement;

public class DarkKnightCombat : MonoBehaviour
{
    [Header("Detección del jugador")]
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 2f;

    [Header("Daño del ataque")]
    [SerializeField] private float hitRange = 1.2f;
    [SerializeField] private float hitHeight = 1f;

    private Transform player;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float nextAttackTime;
    private bool isRestarting = false;

    // Obtiene los componentes necesarios.
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning(
                "DarkKnight: no se encontró el jugador."
            );
        }
    }

    // Comprueba cuándo debe iniciar un ataque.
    private void Update()
    {
        if (player == null || isRestarting)
        {
            return;
        }

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distance <= attackRange &&
            Time.time >= nextAttackTime)
        {
            Attack();
        }
    }

    // Activa la animación y establece el tiempo de espera.
    private void Attack()
    {
        if (animator == null)
        {
            return;
        }

        // Orienta al enemigo hacia el jugador.
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX =
                player.position.x < transform.position.x;
        }

        animator.SetTrigger("Attack");

        nextAttackTime = Time.time + attackCooldown;
    }

    // Este método se ejecuta desde el Animation Event.
    public void ApplyAttackDamage()
    {
        if (player == null || isRestarting)
        {
            return;
        }

        float differenceX =
            player.position.x - transform.position.x;

        float horizontalDistance = Mathf.Abs(differenceX);

        float verticalDistance = Mathf.Abs(
            player.position.y - transform.position.y
        );

        // Comprueba hacia dónde mira el enemigo.
        bool facingRight =
            spriteRenderer != null && !spriteRenderer.flipX;

        bool playerInFront = facingRight
            ? differenceX >= 0f
            : differenceX <= 0f;

        // Comprueba que el jugador esté dentro
        // del alcance efectivo de la espada.
        if (horizontalDistance <= hitRange &&
            verticalDistance <= hitHeight &&
            playerInFront)
        {
            isRestarting = true;

            Debug.Log(
                "DarkKnight: el ataque alcanzó al jugador."
            );

            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }
        else
        {
            Debug.Log(
                "DarkKnight: ataque fallido."
            );
        }
    }
}
