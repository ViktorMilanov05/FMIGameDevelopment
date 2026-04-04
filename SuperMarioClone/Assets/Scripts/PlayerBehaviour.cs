using UnityEngine;
public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    private DeathHandler deathHandler;
    private float bounceForce = 7.5f / 2;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        deathHandler = GetComponent<DeathHandler>();
        animator.SetBool("isDeath", false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out GoombaBehaviour goomba))
        {
            foreach (var contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f && rigidBody.linearVelocity.y <= 0)
                {
                    rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, bounceForce);
                    goomba.Die();
                    break;
                }
                else
                {
                    Die();
                    break;
                }
            }
        }
    }

    public void Die()
    {
        if (animator != null)
            animator.SetBool("isDeath", true);
        deathHandler.Die();
        GameManager.Instance.ResetLevel(3f);
    }
}