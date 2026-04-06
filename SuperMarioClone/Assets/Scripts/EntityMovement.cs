using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField]
    internal float speed = 2f;
    [SerializeField]
    internal Vector2 direction = Vector2.left;

    private Rigidbody2D rigidBody;
    private Vector2 velocity;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    void OnBecameVisible()
    {
        if (gameObject.layer != LayerMask.NameToLayer("Shell"))
        {
            enabled = true;
        }
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Shell") && !collision.gameObject.GetComponent<KoopaShellBehaviour>().Pushed)
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
            return;
        }
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Enemy") && collision.gameObject.layer != LayerMask.NameToLayer("Shell"))
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
     
