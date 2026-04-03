using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpForce = 7.5f;
    [SerializeField]

    private List<GoombaBehaviour> subscribedGoombas = new List<GoombaBehaviour>();
    private Animator animator;
    private PlayerControls controls;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private CameraMovement cameraMovement;
    private Vector2 moveInput;
    private float horizontalInput;
    private float minJumpTimer;
    private float playerHalfWidth;
    private float leftLimit;
    private float groundCheckDistance = 0.1f;
    private bool isJumpRequested;
    private bool isJumpHeld;
    private bool isGrounded;
    private bool wasGrounded;


    void Awake()
    {
        animator = GetComponent<Animator>();
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Jump.performed += ctx =>
        {
            isJumpRequested = true;
            isJumpHeld = true;
            animator.SetBool("isJumping", true);

        };

        controls.Player.Jump.canceled += ctx =>
        {
            isJumpHeld = false;
        };
    }

    void OnEnable()
    {
        controls.Player.Enable();
        foreach (var goomba in FindObjectsByType<GoombaBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            goomba.OnSmashed += BounceAfterEnemyHit;
            subscribedGoombas.Add(goomba);
        }

    }
    void OnDisable() 
    {
        controls.Player.Disable();
        foreach (var goomba in subscribedGoombas)
        {
            if (goomba != null)
                goomba.OnSmashed -= BounceAfterEnemyHit;
        }
        subscribedGoombas.Clear();
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        cameraMovement = Camera.main.GetComponent<CameraMovement>();
    }

    void Update()
    {
        horizontalInput = moveInput.x;
        playerHalfWidth = boxCollider.bounds.extents.x;
        leftLimit = cameraMovement.leftLimit + playerHalfWidth;

        if (horizontalInput < 0 && rigidBody.position.x <= leftLimit)
        {
            horizontalInput = 0;
        }
        animator.SetBool("isRunning", horizontalInput != 0);

        if (!wasGrounded && isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
    }

    void FixedUpdate()
    {

        Vector2 velocity = rigidBody.linearVelocity;
        velocity.x = horizontalInput * speed;
        rigidBody.linearVelocity = velocity;

        if (horizontalInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        wasGrounded = isGrounded;
        isGrounded = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, groundCheckDistance, groundLayer);

        if (isJumpRequested && isGrounded)
        {
            Jump();
            isJumpRequested = false;
        }

        if (!isJumpHeld && rigidBody.linearVelocity.y > 0)
        {
            rigidBody.linearVelocity = new Vector2(
                rigidBody.linearVelocity.x,
                rigidBody.linearVelocity.y * 0.9f
            );
        }
    }
    public void BounceAfterEnemyHit()
    {
        rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpForce / 2);
    }
    void Jump()
    {
        rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpForce);
    }
}