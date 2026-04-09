using System.Collections;
using UnityEngine;

public class BlockItem : MonoBehaviour
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

        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = transform.localPosition + Vector3.up;
        while (elapsed < duration)
        {
            float percentOfAnimation = elapsed / duration;

            transform.localPosition = Vector3.Lerp(startPosition,endPosition, percentOfAnimation);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = endPosition;

        rigidBody.simulated = true;
        physicsCollider.enabled = true;
        triggerCollider.enabled = true;
    }
}
