using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    [SerializeField]
    private int maxHits = -1;
    [SerializeField]
    private Sprite emptyBlock;
    [SerializeField]
    private GameObject item;

    private SpriteRenderer spriteRenderer;
    private bool animating = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerBehaviour player = collision.gameObject.GetComponent<PlayerBehaviour>();
            Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
            foreach (var contact in collision.contacts)
            {
                if (!animating && maxHits != 0 && contact.normal.y > 0.5f)
                {
                    Hit();
                    break;
                }
            }
        }
    }

    void Hit()
    {
        spriteRenderer.enabled = true;
        maxHits--;
        if(maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }
        if(item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
        StartCoroutine(Animate());


    }

    IEnumerator Animate()
    {
        animating = true;
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;
        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);
        animating = false;
    }

    IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;
        while (elapsed < duration)
        {
            float percentOfAnimation = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, percentOfAnimation);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }
}
