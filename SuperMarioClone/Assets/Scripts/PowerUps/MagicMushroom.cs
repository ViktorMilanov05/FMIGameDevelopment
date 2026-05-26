public class MagicMushroom : PowerUpBase
{
    protected override void Collect(PlayerBehaviour player)
    {
        player.Grow();
    }
}
