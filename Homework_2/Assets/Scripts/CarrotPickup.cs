using UnityEngine;

public class CarrotPickup : MonoBehaviour
{
    [SerializeField]
    private CarrotUI carrotUI;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            carrotUI.AddCarrot();
            Destroy(gameObject);
        } 
    }
}
