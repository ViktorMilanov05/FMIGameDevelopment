using UnityEngine;

public class GoombaBehaviour : MonoBehaviour
{
    private Animator animator;
    private DeathHandler deathHandler;
    private Vector3 shashedGoombaSpriteOffset = new(0, -0.215f, 0);

    private void Awake()
    {
        animator = GetComponent<Animator>();
        deathHandler = GetComponent<DeathHandler>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerBehaviour player = collision.gameObject.GetComponent<PlayerBehaviour>();
            Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
            if (player.Starpower)
            {
                Hit();
            }
            else if (collision.contacts[0].normal.y < -0.5f && playerRigidbody.linearVelocity.y <= 0)
            {
                player.BounceAfterEnemyHit();
                Die();
            }
            else
            {
                player.Hit();
            }

        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Shell") && collision.gameObject.GetComponent<KoopaShellBehaviour>().Pushed)
        {
            Hit();
        }
    }

    void Hit()
    {
        animator.enabled = false;
        deathHandler.Die();
        Destroy(gameObject, 3f);
    }

    void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        animator.SetBool("isSmashed", true);
        transform.position += shashedGoombaSpriteOffset;
        Destroy(gameObject, 0.5f);
    }
}
