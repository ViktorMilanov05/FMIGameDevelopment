using System.Collections;
using UnityEngine;

public class BlockCoin : SpawnedItem
{
    void Start()
    {
        GameManager.Instance.AddCoin();
        StartCoroutine(Animate());
    }
    IEnumerator Animate()
    {
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f;
        yield return Move(restingPosition, animatedPosition, 0.25f);
        yield return Move(animatedPosition, restingPosition, 0.25f);
        Destroy(gameObject);
    }
}
