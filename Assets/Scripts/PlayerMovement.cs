using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 14.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float horizontalInput;
    private bool isGrounded = true;
    private int cakeCount = 0;
    private int chickenCount = 0;
    private int coffeeCount = 0;
    private int jamCount = 0;
    private int cookieCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            isGrounded = false;
            animator.SetBool("IsJumping", true);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            horizontalInput * moveSpeed,
            rb.linearVelocity.y
        );
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Collectible"))
        {
            return;
        }

        switch (other.gameObject.name)
        {
            case "Cake":
                cakeCount++;
                break;

            case "Chicken":
                chickenCount++;
                break;

            case "Coffee":
                coffeeCount++;
                break;

            case "Jam":
                jamCount++;
                break;

            case "Cookie":
                cookieCount++;
                break;

            default:
                return;
        }

        Debug.Log(
            $"Cake: {cakeCount} | Chicken: {chickenCount} | " +
            $"Coffee: {coffeeCount} | Jam: {jamCount} | Cookie: {cookieCount}"
        );

        Destroy(other.gameObject);
    }

}