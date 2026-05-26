using UnityEngine;

public class Goomba : EnemyBase
{
    private Animator animator;
    private Vector3 shashedGoombaSpriteOffset = new(0, -0.215f, 0);

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public override void Hit()
    {
        animator.enabled = false;
        deathHandler.Die();
        Destroy(gameObject, 3f);
    }

    protected override void OnPlayerBounce(PlayerBehaviour player)
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        animator.SetBool("isSmashed", true);
        transform.position += shashedGoombaSpriteOffset;
        Destroy(gameObject, 0.5f);
    }
}
