using System;
using System.Collections;
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

    [SerializeField]
    private float powerUpDuration = 3f;

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
    private PlayerControls controls;
    private Vector2 moveInput;
    private bool canKillEnemies = false;
    private Vector3 startScale;
    private float currentPowerUpScale = 1f;

    public Action OnPowerUpActivated;
    public static Action OnPlayerDamaged;
    public static Action OnEnemyKilled;
    internal Rigidbody2D GetRigidBody() => rigidBody;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        controls.Player.Move.canceled += contex => moveInput = Vector2.zero;

        controls.Player.Jump.performed += contex =>
        {
            animator.SetBool("isJumping", true);
            isJumpRequested = true;
        };
    }

    void OnEnable()
    {
        if (controls != null)
            controls.Enable();
    }

    void OnDisable()
    {
        if (controls != null)
            controls.Disable();
    }
    internal void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        startScale = transform.localScale;
    }

    void Update()
    {
        horizontalInput = moveInput.x;

        animator.SetBool("isWalking", horizontalInput != 0);

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
                transform.localScale = new Vector3(1,1,1) * currentPowerUpScale;
            }
            else if(horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1,1,1) * currentPowerUpScale;
            }
        }

        wasGrounded = isGrounded;
        isGrounded = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, groundCheckDistance, groundLayer);

        if (isJumpRequested && isGrounded)
        {
            Jump();
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

    internal void Jump()
    {
        rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (canKillEnemies)
            {
                collision.gameObject.GetComponent<EnemyAI>()?.Die();
                OnEnemyKilled?.Invoke();
            }
            else if (!isInvincible)
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
        OnPlayerDamaged?.Invoke();
        animator.SetTrigger("Hurt");
        isInvincible = true;
        invincibilityTimer = invincibilityTime; 
    }

    public void ActivatePowerUp()
    {
        canKillEnemies = true;
        currentPowerUpScale = 1.2f;
        transform.localScale = Vector3.one * currentPowerUpScale;
        StartCoroutine(PowerUpTimer());
    }
    
    private IEnumerator PowerUpTimer()
    {
        yield return new WaitForSeconds(powerUpDuration);
        canKillEnemies = false;
        currentPowerUpScale = 1f;
        transform.localScale = startScale;
    }
}
