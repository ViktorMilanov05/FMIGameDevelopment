using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    internal float leftLimit;
    public event Action<bool> OnUndergroundChanged;

    [SerializeField]
    private float undergroundHeight = -9f;
    [SerializeField]
    private float height = 6f;

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

    internal void SetUnderground(bool underground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position = cameraPosition;

        OnUndergroundChanged?.Invoke(underground);
    }
}
