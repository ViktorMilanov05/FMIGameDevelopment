using UnityEngine;

public class KoopaShellBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    private EntityMovement movement;
    internal bool Pushed { get; private set; }

    private void Awake()
    {
        movement = GetComponent<EntityMovement>();
        movement.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerBehaviour player = collision.gameObject.GetComponent<PlayerBehaviour>();
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            bool fromAbove = collision.contacts[0].normal.y < -0.5f && playerRb.linearVelocity.y <= 0;

            if (fromAbove)
            {
                player.BounceAfterEnemyHit();
            }

            if (!Pushed)
            {
                Vector2 dir = new Vector2(transform.position.x - player.transform.position.x, 0f).normalized;
                PushShell(dir);
            }
            else
            {
                player.Die();
            }
        }
    }

    private void PushShell(Vector2 direction)
    {
        Pushed = true;
        movement.direction = direction;
        movement.speed = speed;
        movement.enabled = true;
    }

    void OnBecameInvisible()
    {
        if (Pushed)
        {
            Destroy(gameObject);
        }
    }
}