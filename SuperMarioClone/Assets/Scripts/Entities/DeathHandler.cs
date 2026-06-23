using System.Collections;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Collider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Die(float duration = 3f, float jumpVelocity = 8f, float gravity = -20f)
    {
        spriteRenderer.sortingOrder = 10;
        boxCollider.enabled = false;
        rigidBody.simulated = false;
        StartCoroutine(AnimateDeath(duration, jumpVelocity, gravity));
    }

    private IEnumerator AnimateDeath(float duration, float jumpVelocity, float gravity)
    {
        float elapsed = 0f;
        Vector3 velocity = Vector3.up * jumpVelocity;

        while (elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}