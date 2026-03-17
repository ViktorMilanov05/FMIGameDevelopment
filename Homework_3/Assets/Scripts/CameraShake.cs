using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeTime;
    private float shakeStrength;

    private Vector3 startPosition;
    private float noiseSeed;

    void Start()
    {
        startPosition = transform.localPosition;
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

    void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;

            float x = (Mathf.PerlinNoise(noiseSeed, Time.time * 25f) - 0.5f) * shakeStrength;
            float y = (Mathf.PerlinNoise(noiseSeed + 1, Time.time * 25f) - 0.5f) * shakeStrength;

            transform.localPosition = startPosition + new Vector3(x, y, 0);
        }
        else
        {
            transform.localPosition = startPosition;
        }
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