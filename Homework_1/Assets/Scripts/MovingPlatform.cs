using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 moveDirection = Vector2.right;
    public float moveDistance = 5f;
    public float moveSpeed = 2f;

    private Rigidbody2D rigidBody;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private bool movingToEnd = true;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
        startPosition = transform.position;
        endPosition = startPosition + moveDirection.normalized * moveDistance;
    }

    void FixedUpdate()
    {
        Vector2 target = movingToEnd ? endPosition : startPosition;
        Vector2 newPosition = Vector2.MoveTowards(rigidBody.position, target, moveSpeed * Time.fixedDeltaTime);

        rigidBody.MovePosition(newPosition);

        if(Vector2.Distance(newPosition, target) < 0.01f)
        {
            movingToEnd = !movingToEnd;
        }
    }
}
