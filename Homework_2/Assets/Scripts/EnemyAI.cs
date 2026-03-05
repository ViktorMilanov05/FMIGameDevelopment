using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private float detectionRange = 3f;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundCheckDistance = 0.5f;

    [SerializeField]
    private LayerMask groundLayer;

    private Transform player;
    private Animator animator;
    private Rigidbody2D rigidbodyEnemy;
    private BoxCollider2D boxCollider;
    private Vector2 startPosition;
    private bool returningToStart = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rigidbodyEnemy = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            returningToStart = false;
        }
        else
        {
            returningToStart = true;
        }
        animator.SetBool("isWalking", Mathf.Abs(rigidbodyEnemy.linearVelocity.x) > 0.1f);
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (!returningToStart)
        {
            ChasePLayer();
        }
        else
        {
            ReturnToStart();
        }
    }
    void ChasePLayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        
        FaceDirection(direction.x);
        
        if (IsGroundAhead())
        {
            rigidbodyEnemy.linearVelocity = new Vector2(direction.x * moveSpeed, rigidbodyEnemy.linearVelocity.y);
        }
        else
        {
            rigidbodyEnemy.linearVelocity = new Vector2(0, rigidbodyEnemy.linearVelocity.y);
        }
    }

    void ReturnToStart()
    {
        float distanceToStart = Vector2.Distance(transform.position, startPosition);

        if(distanceToStart < 0.1f)
        {
            rigidbodyEnemy.linearVelocity = new Vector2(0, rigidbodyEnemy.linearVelocity.y);
            return;
        }
        Vector2 direction = (startPosition - (Vector2)transform.position).normalized;
        FaceDirection(direction.x);

        if (IsGroundAhead())
        {
            rigidbodyEnemy.linearVelocity = new Vector2(direction.x * moveSpeed, rigidbodyEnemy.linearVelocity.y);
        }
        else
        {
            rigidbodyEnemy.linearVelocity = new Vector2(0, rigidbodyEnemy.linearVelocity.y);
        }
    }
    void FaceDirection(float xDirection)
    {
        if (xDirection > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (xDirection < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    bool IsGroundAhead()
    {
        Vector2 boxSize = boxCollider.bounds.size;

        RaycastHit2D hit = Physics2D.BoxCast(groundCheck.position, boxSize, 0f, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }
}
