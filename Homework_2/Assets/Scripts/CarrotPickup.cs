using System;
using UnityEngine;

public class CarrotPickup : MonoBehaviour
{
    public static Action OnCarrotCollected;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnCarrotCollected?.Invoke();
            Destroy(gameObject);
        } 
    }
}
