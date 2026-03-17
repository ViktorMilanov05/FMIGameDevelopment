using UnityEngine;

public class JumpingPad : MonoBehaviour
{
    [SerializeField]
    private float bounceForce = 15f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRigidBody != null)
            {
                playerRigidBody.linearVelocity = new Vector2(playerRigidBody.linearVelocity.x, 0f);
                playerRigidBody.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}
