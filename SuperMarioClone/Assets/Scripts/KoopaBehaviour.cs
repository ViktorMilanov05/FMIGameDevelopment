using UnityEngine;

public class KoopaBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject shellPrefab;

    private Vector3 shashedGoombaSpriteOffset = new(0, -0.28f, 0);
    private DeathHandler deathHandler;

    private void Awake()
    {
        deathHandler = GetComponent<DeathHandler>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerBehaviour player = collision.gameObject.GetComponent<PlayerBehaviour>();
            Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
            if (collision.contacts[0].normal.y < -0.5f && playerRigidbody.linearVelocity.y <= 0)
            {
                player.BounceAfterEnemyHit();
                EnterShell();
            }
            else
            {
                player.Die();
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Shell") && collision.gameObject.GetComponent<KoopaShellBehaviour>().Pushed)
        {
            Hit();
        }
    }
    private void EnterShell()
    {
        Instantiate(shellPrefab, transform.position + shashedGoombaSpriteOffset, Quaternion.identity);
        Destroy(gameObject);
    }
    void Hit()
    {
        deathHandler.Die();
        Destroy(gameObject, 3f);
    }
}