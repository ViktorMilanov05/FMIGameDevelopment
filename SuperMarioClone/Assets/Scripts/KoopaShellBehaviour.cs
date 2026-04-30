using UnityEngine;

public class KoopaShellBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    private EntityMovement movement;
    private DeathHandler deathHandler;

    internal bool Pushed { get; private set; }

    private void Awake()
    {
        movement = GetComponent<EntityMovement>();
        deathHandler = GetComponent<DeathHandler>();
        movement.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerBehaviour player = collision.gameObject.GetComponent<PlayerBehaviour>();
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            foreach (var contact in collision.contacts)
            {
                if (contact.normal.y < -0.5f && playerRb.linearVelocity.y <= 0)
                {
                    player.BounceAfterEnemyHit();
                }
            }

            if (!Pushed)
            {
                Vector2 dir = new Vector2(transform.position.x - player.transform.position.x, 0f).normalized;
                PushShell(dir);
            }
            else
            {
                if (player.Starpower)
                {
                    Hit();
                }
                player.Hit();
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

    void Hit()
    {
        deathHandler.Die();
        Destroy(gameObject, 3f);
    }

    void OnBecameInvisible()
    {
        if (Pushed)
        {
            Destroy(gameObject);
        }
    }
}