
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpForce = 5f;

    private PlayerControls controls;
    private Vector2 moveInput;
    private Rigidbody2D rigidBody;
    private float horizontalInput;

    void Awake()
    {
        controls = new PlayerControls();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        controls.Player.Enable();

        controls.Player.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        controls.Player.Move.canceled += contex => moveInput = Vector2.zero;
        controls.Player.Jump.performed += context => Jump();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    private void Update()
    {
        horizontalInput = moveInput.x;
    }

    void FixedUpdate()
    {
        if (horizontalInput != 0)
        {
            rigidBody.linearVelocity = new Vector2(horizontalInput * speed, rigidBody.linearVelocity.y);

            if (horizontalInput > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    void Jump()
    {
        rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpForce);
    }
}
