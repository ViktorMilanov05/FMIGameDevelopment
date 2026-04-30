using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    private TextMeshProUGUI livesText;
    private CameraMovement cameraScript;

    void Awake()
    {
        livesText = GetComponent<TextMeshProUGUI>();
    }
    void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLivesChanged -= UpdateText;
        }
    }

    void Start()
    {
        cameraScript = Camera.main.GetComponent<CameraMovement>();
        cameraScript.OnUndergroundChanged += ChangeColor;
        var gm = GameManager.Instance;

        if (gm != null)
        {
            GameManager.Instance.OnLivesChanged += UpdateText;
            UpdateText(gm.Lives);
        }
    }

    void UpdateText(int lives)
    {
        livesText.text = $"Lives: {lives}";
    }

    void ChangeColor(bool underground)
    {
        livesText.color = underground ? Color.white : Color.black;
    }
}