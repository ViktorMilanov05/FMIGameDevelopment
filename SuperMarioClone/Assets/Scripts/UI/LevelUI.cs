public class LevelUI : GameUIBase
{
    protected override void Subscribe()
    {
        var gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            return;
        }
        gameManager.OnLevelChanged += UpdateText;
        UpdateText(gameManager.Level);
    }

    protected override void Unsubscribe()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLevelChanged -= UpdateText;
        }
    }

    void UpdateText(int level)
    {
        label.text = $"Level: {level}";
    }
}