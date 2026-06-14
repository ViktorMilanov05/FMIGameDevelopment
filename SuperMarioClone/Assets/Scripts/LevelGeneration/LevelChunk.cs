using UnityEngine;

public class LevelChunk : MonoBehaviour
{
    [SerializeField]
    private Transform endPoint;

    public Vector3 EndPoint => endPoint.position;
}
