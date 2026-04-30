using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        Starpower
    }

    public Type type;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect(collision.gameObject);
        }    
    }

    void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Coin:
                GameManager.Instance.AddCoin();
                break;
            case Type.ExtraLife:
                GameManager.Instance.AddLife();
                break;
            case Type.MagicMushroom:
                player.GetComponent<PlayerBehaviour>().Grow();
                break;
            case Type.Starpower:
                player.GetComponent<PlayerBehaviour>().GetStarpower(10);
                break;
        }
        Destroy(gameObject);
    }
}
