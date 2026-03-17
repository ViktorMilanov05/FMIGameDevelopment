using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 3;

    [SerializeField]
    internal Image[] hearths = null;

    [SerializeField]
    internal Image redVignetteImage = null;

    private Material redVignetteMaterial;

    private int currentHealth;

    public static Action OnPlayerDeath;
    public int CurrentHealth() => currentHealth;
    internal void Start()
    {
        currentHealth = maxHealth;
        redVignetteMaterial = redVignetteImage.material;
        UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
        UpdateHearts();
    }

    void UpdateHearts()
    {
        for(int i = 0; i < hearths.Length; i++)
        {
            hearths[i].enabled = i < currentHealth;
        }
        if (currentHealth <= 1)
        {
            redVignetteMaterial.SetFloat("_Intensity", 0.8f);
        }
        else
        {
            redVignetteMaterial.SetFloat("_Intensity", 0f);
        }
    }
}
