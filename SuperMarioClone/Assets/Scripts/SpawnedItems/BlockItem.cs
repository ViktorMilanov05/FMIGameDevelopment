using System.Collections;
using UnityEngine;

public class BlockItem : SpawnedItem
{

    private Rigidbody2D rigidBody;
    private CircleCollider2D physicsCollider;
    private BoxCollider2D triggerCollider;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<CircleCollider2D>();
        triggerCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        rigidBody.simulated = false;
        physicsCollider.enabled = false;
        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f);

        spriteRenderer.enabled = true;

        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = transform.localPosition + Vector3.up;
        yield return Move(startPosition, endPosition, 0.5f);

        rigidBody.simulated = true;
        physicsCollider.enabled = true;
        triggerCollider.enabled = true;
    }
}
