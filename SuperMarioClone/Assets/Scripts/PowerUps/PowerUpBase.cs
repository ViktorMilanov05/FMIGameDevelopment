using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.GetComponent<PlayerBehaviour>());
            Destroy(gameObject);
        }
    }

    protected abstract void Collect(PlayerBehaviour player);
}
