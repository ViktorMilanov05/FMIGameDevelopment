using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    internal float leftLimit;

    private Transform player;
    private Camera mainCamera;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        transform.position = cameraPosition;

        float halfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        leftLimit = transform.position.x - halfWidth;
    }
}
