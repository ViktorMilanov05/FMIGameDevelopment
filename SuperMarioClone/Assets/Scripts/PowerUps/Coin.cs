public class Coin : PowerUpBase
{
    protected override void Collect(PlayerBehaviour player)
    {
        GameManager.Instance.AddCoin();
    }
}