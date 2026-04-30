using System.Collections;
using System.Timers;
using UnityEngine;
public class PlayerBehaviour : MonoBehaviour
{
    public bool Starpower { get; private set; }

    private Rigidbody2D rigidBody;
    private Animator animator;
    private DeathHandler deathHandler;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private float bounceForce = 7.5f / 2;
    private Vector2 bigMarioSpriteOffset = new(0, 0.5f);

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        deathHandler = GetComponent<DeathHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetBool("isDeath", false);
    }

    public void BounceAfterEnemyHit()
    {
        rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, bounceForce);
    }

    public void Grow()
    {

        animator.SetBool("isBig", true);
        boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y * 2f);
        rigidBody.MovePosition(rigidBody.position + bigMarioSpriteOffset);

    }
    public void Shrink()
    {

        animator.SetBool("isBig", false);
        boxCollider.size = new Vector2(boxCollider.size.x, boxCollider.size.y / 2f);
        rigidBody.MovePosition(rigidBody.position - bigMarioSpriteOffset);
    }
    public void Hit()
    {
        if (animator.GetBool("isBig"))
        {
            Shrink();
            return;
        }
        animator.SetBool("isDeath", true);
        deathHandler.Die();
        GameManager.Instance.ResetLevel(3f);
    }

    public void StartHolding()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isHolding", true);
    }

    public void StopHolding()
    {
        animator.SetBool("isRunning", true);
        animator.SetBool("isHolding", false);
    }

    public bool isBig()
    {
        return animator.GetBool("isBig");
    }

    public void GetStarpower(float duration = 10f)
    {
        StartCoroutine(StarpowerAnimation(duration));

    }

    private IEnumerator StarpowerAnimation(float duration)
    {
        Starpower = true;

        float elapsed = 0f;

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if(Time.frameCount % 10 == 0)
            {
                spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }
            yield return null;
        }
        spriteRenderer.color = Color.white;
        Starpower = false;
    }
}