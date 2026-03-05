using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float jumpForce = 10f;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float groundCheckDistance = 0.1f;

    [SerializeField]
    private float fallThreshold = -10f;

    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private float invincibilityTime = 1f;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private bool isGrounded;
    private bool wasGrounded;
    private bool isJumpRequested;
    private Vector3 startPosition;
    private Animator animator;
    private bool isInvincible = false;
    private float invincibilityTimer;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        animator.SetBool("isWalking", horizontalInput != 0);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("isJumping", true);
            isJumpRequested = true;
        }

        if (!wasGrounded && isGrounded)
        {
            animator.SetBool("isJumping", false);
        }

        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if(invincibilityTimer <= 0f)
            {
                isInvincible = false;
            }
        }
    }
    void FixedUpdate()
    {
        if (horizontalInput != 0)
        {
            rigidBody.linearVelocity = new Vector2 (horizontalInput * moveSpeed, rigidBody.linearVelocity.y);

            if(horizontalInput > 0)
            {
                transform.localScale = new Vector3(1,1,1);
            }
            else if(horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1,1,1);
            }
        }

        wasGrounded = isGrounded;
        isGrounded = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, groundCheckDistance, groundLayer);

        if (isJumpRequested && isGrounded)
        {
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpForce);
            isJumpRequested = false;
        }

        if (transform.position.y < fallThreshold)
        {
            rigidBody.linearVelocity = Vector2.zero;
            transform.position = startPosition;
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isInvincible)
            {
                TakeDamageFromEnemy();
            }
        }
    }
    
    void TakeDamageFromEnemy()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1);
        }
        animator.SetTrigger("Hurt");
        isInvincible = true;
        invincibilityTimer = invincibilityTime; 
    }
}
