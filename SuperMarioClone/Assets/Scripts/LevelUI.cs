using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    private TextMeshProUGUI levelText;
    private CameraMovement cameraScript;

    void Awake()
    {
        levelText = GetComponent<TextMeshProUGUI>();
    }
    void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLevelChanged -= UpdateText;
        }
    }

    void Start()
    {
        cameraScript = Camera.main.GetComponent<CameraMovement>();
        cameraScript.OnUndergroundChanged += ChangeColor;
        var gm = GameManager.Instance;

        if (gm != null)
        {
            GameManager.Instance.OnLevelChanged += UpdateText;
            UpdateText(gm.Level);
        }
    }

    void UpdateText(int Level)
    {
        levelText.text = $"Level: {Level}";
    }

    void ChangeColor(bool underground)
    {
        levelText.color = underground ? Color.white : Color.black;
    }
}

