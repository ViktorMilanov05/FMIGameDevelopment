using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.1f;
    public float fallThreshold = -10f;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private bool isGrounded;
    private bool isJumpRequested;
    private Vector3 startPosition;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            isJumpRequested = true;
        }
    }
    void FixedUpdate()
    {
        if (horizontalInput != 0)
        {
            rigidBody.linearVelocity = new Vector2 (horizontalInput * moveSpeed, rigidBody.linearVelocity.y);
        }

        isGrounded = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, groundCheckDistance, groundLayer);

        if (isJumpRequested && isGrounded)
        {
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpForce);
            isJumpRequested = false;
        }

        if(transform.position.y < fallThreshold)
        {
            rigidBody.linearVelocity = Vector2.zero;
            transform.position = startPosition;
        }
    }
}
