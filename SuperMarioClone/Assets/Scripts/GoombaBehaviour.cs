using System;
using UnityEngine;

public class GoombaBehaviour : MonoBehaviour
{
    public event Action OnSmashed;

    private Animator animator;
    private Vector3 shashedGoombaSpriteOffset = new(0, -0.215f, 0);

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            PlayerMovements player = collision.gameObject.GetComponent<PlayerMovements>();

            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y < -0.5f && playerRb.linearVelocity.y <= 0)
                {
                    Die();
                    OnSmashed?.Invoke();
                    break;
                }
            }
        }
    }

    internal void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        animator.SetBool("isSmashed", true);
        transform.position += shashedGoombaSpriteOffset;
        OnSmashed?.Invoke();
        Destroy(gameObject, 0.5f);
    }
}
