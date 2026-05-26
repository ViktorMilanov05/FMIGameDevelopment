public class ExtraLifeMushroom : PowerUpBase
{
    protected override void Collect(PlayerBehaviour player)
    {
        GameManager.Instance.AddLife();
    }
}