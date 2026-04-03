using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private Vector2 direction = Vector2.left;

    private Rigidbody2D rigidBody;
    private Vector2 velocity;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    void OnBecameVisible()
    {
        enabled = true;
    }

    void OnBecameInvisible()
    {
        enabled = false;
    }
    void OnEnable()
    {
        rigidBody.WakeUp();
    }

    void OnDisable()
    {
        rigidBody.linearVelocity = Vector2.zero;
        rigidBody.Sleep();
    }

    void FixedUpdate()
    {
        velocity = rigidBody.linearVelocity;
        velocity.x = direction.x * speed;

        rigidBody.linearVelocity = velocity;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (!collision.gameObject.CompareTag("Player"))
            {
                if (Mathf.Abs(contact.normal.x) > 0.5f)
                {
                    direction.x *= -1f;
                    break;
                }
            }
        }
    }
}
     
