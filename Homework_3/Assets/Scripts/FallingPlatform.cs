using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField]
    private float fallDelay = 0.5f;
    
    [SerializeField]
    private float resetDelay = 5f;

    private Rigidbody2D rigidBody;
    private Vector3 startPosition;
    private bool isFalling = false;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
    }

   void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isFalling && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallAfterDelay());
        }
    }

    IEnumerator FallAfterDelay()
    {
        isFalling = true;
        yield return new WaitForSeconds(fallDelay);
        rigidBody.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(resetDelay);
        rigidBody.bodyType = RigidbodyType2D.Kinematic;
        rigidBody.linearVelocity = Vector2.zero;
        rigidBody.angularVelocity = 0f;
        rigidBody.rotation = 0f;
        transform.position = startPosition;

        isFalling = false;
    }
}
