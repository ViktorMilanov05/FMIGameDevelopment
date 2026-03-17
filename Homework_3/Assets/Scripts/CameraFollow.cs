using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float smoothSpeed = 5f;

    private float shakeTime;
    private float shakeStrength;
    private float noiseSeed;

    void Start()
    {
        noiseSeed = Random.Range(0f, 100f);
    }

    void OnEnable()
    {
        PlayerMovement.OnPlayerDamaged += StrongShake;
        PlayerMovement.OnEnemyKilled += WeakShake;
    }

    void OnDisable()
    {
        PlayerMovement.OnPlayerDamaged -= StrongShake;
        PlayerMovement.OnEnemyKilled -= WeakShake;
    }

    void LateUpdate()
    {
        Vector3 wantedPosition = target.position + offset;

        Vector3 shakeOffset = Vector3.zero;
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;

            float x = (Mathf.PerlinNoise(noiseSeed, Time.time * 25f) - 0.5f) * shakeStrength;
            float y = (Mathf.PerlinNoise(noiseSeed + 1, Time.time * 25f) - 0.5f) * shakeStrength;
            shakeOffset = new Vector3(x, y, 0);
        }

        transform.position = Vector3.Lerp(transform.position, wantedPosition, smoothSpeed * Time.deltaTime) + shakeOffset;
    }

    void StrongShake()
    {
        shakeTime = 0.25f;
        shakeStrength = 0.35f;
    }

    void WeakShake()
    {
        shakeTime = 0.15f;
        shakeStrength = 0.15f;
    }
}