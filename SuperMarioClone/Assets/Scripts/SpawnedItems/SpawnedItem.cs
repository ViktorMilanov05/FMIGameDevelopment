using System.Collections;
using UnityEngine;

public abstract class SpawnedItem : MonoBehaviour
{
    protected IEnumerator Move(Vector3 from, Vector3 to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.localPosition = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = to;
    }
}
