using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 3;

    [SerializeField]
    private Image[] hearths;

    [SerializeField]
    private Image redVignetteImage;

    private Material redVignetteMaterial;

    private int currentHealth;
    void Start()
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
            SceneManager.LoadScene("MainMenu");
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
