public class CoinsUI : GameUIBase
{
    protected override void Subscribe()
    {
        var gameManager = GameManager.Instance;
        if(gameManager == null)
        {
            return;
        }
        gameManager.OnCoinsChanged += UpdateText;
        UpdateText(gameManager.Coins);
    }

    protected override void Unsubscribe()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnCoinsChanged -= UpdateText;
        }
    }

    void UpdateText(int coins)
    {
        label.text = $"Coins: {coins}";
    }
}