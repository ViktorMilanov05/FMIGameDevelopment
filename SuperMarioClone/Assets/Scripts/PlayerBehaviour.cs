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

    public void BounceAfterEnemyHit()
    {
        rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, bounceForce);
    }
    public void Die()
    {
        if (animator != null)
            animator.SetBool("isDeath", true);
        deathHandler.Die();
        GameManager.Instance.ResetLevel(3f);
    }
}