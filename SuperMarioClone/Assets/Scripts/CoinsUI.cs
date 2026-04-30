using TMPro;
using UnityEngine;

public class CoinsUI : MonoBehaviour
{
    private TextMeshProUGUI coinsText;
    private CameraMovement cameraScript;

    void Awake()
    {
        coinsText = GetComponent<TextMeshProUGUI>();
    }
    void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnCoinsChanged -= UpdateText;
        }
    }

    void Start()
    {
        cameraScript = Camera.main.GetComponent<CameraMovement>();
        cameraScript.OnUndergroundChanged += ChangeColor;
        var gm = GameManager.Instance;

        if (gm != null)
        {
            GameManager.Instance.OnCoinsChanged += UpdateText;
            UpdateText(gm.Coins);
        }
    }

    void UpdateText(int coins)
    {
        coinsText.text = $"Coins: {coins}";
    }

    void ChangeColor(bool underground)
    {
        coinsText.color = underground ? Color.white : Color.black;
    }
}