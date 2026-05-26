using UnityEngine;

public class Koopa : EnemyBase
{
    [SerializeField]
    private GameObject shellPrefab;
    private Vector3 shelledKoombaSpriteOffset = new(0, -0.28f, 0);
    public override void Hit()
    {
        deathHandler.Die();
        Destroy(gameObject, 3f);
    }

    protected override void OnPlayerBounce(PlayerBehaviour player)
    {
        EnterShell();
    }
    private void EnterShell()
    {
        Instantiate(shellPrefab, transform.position + shelledKoombaSpriteOffset, Quaternion.identity);
        Destroy(gameObject);
    }

}