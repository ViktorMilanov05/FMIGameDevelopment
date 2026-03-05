using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField] 
    private Vector3 offset;

    [SerializeField] 
    private float smoothSpeed = 5f;

    private void LateUpdate()
    {
        Vector3 wantedPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, wantedPosition, smoothSpeed * Time.deltaTime);
    }
}
