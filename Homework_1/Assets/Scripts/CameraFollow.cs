using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 5f;

    private void LateUpdate()
    {
        Vector3 wantedPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, wantedPosition, smoothSpeed * Time.deltaTime);
    }
}
