using System;
using UnityEngine;

public class GoombaBehaviour : MonoBehaviour
{
    private Animator animator;
    private Vector3 shashedGoombaSpriteOffset = new(0, -0.215f, 0);

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    internal void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        animator.SetBool("isSmashed", true);
        transform.position += shashedGoombaSpriteOffset;
        Destroy(gameObject, 0.5f);
    }
}
