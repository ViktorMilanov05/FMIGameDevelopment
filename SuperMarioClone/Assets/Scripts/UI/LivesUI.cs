public class LivesUI : GameUIBase
{
    protected override void Subscribe()
    {
        var gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            return;
        }
        gameManager.OnLivesChanged += UpdateText;
        UpdateText(gameManager.Lives);
    }

    protected override void Unsubscribe()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLivesChanged -= UpdateText;
        }
    }

    void UpdateText(int lives)
    {
        label.text = $"Lives: {lives}";
    }
}