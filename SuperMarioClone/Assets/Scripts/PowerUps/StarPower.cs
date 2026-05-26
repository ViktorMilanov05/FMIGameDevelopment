public class StarPower : PowerUpBase
{
    protected override void Collect(PlayerBehaviour player)
    {
        player.GetStarpower(10);
    }
}
