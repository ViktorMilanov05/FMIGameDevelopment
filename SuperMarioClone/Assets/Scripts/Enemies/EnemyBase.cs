using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected DeathHandler deathHandler;

    protected virtual void Awake()
    {
        deathHandler = GetComponent<DeathHandler>();
    }

    public abstract void Hit();
    protected abstract void OnPlayerBounce(PlayerBehaviour player);

    protected virtual void OnCollisionEnter2D(Collision2D collision)
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
                OnPlayerBounce(player);
            }
            else
            {
                player.Hit();
            }
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Shell") && collision.gameObject.GetComponent<KoopaShell>().Pushed)
        {
            Hit();
        }
    }
}
