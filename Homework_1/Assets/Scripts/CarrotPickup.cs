using UnityEngine;

public class CarrotPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("Collected carrot!");
            Destroy(gameObject);
        } 
    }
}
