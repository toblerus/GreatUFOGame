using UnityEngine;
using System.Linq;

public class PlayerBullet : BaseBullet
{
    public override void DestroyBullet()
    {
        Destroy(gameObject);
    }

    protected override bool CanCollide(GameObject collidedObjects)
    {
        var bossHealth = collidedObjects.GetComponent<BossHealth>();
        var bullet = collidedObjects.GetComponent<BaseBullet>();
        return bossHealth != null || bullet != null && bullet.BulletType == BulletType.Bomb;
    }

    protected override void OnCollide(GameObject collidedObjects)
    {
        var bossHealth = collidedObjects.GetComponent<BossHealth>();
        if (bossHealth)
        {
            bossHealth.Damage(Damage, null);

            if (!bossHealth.IsDead && !bossHealth.IsInvincible)
                RewardAi();
        }
        
        DestroyBullet();
    }

    private void RewardAi()
    {
        var owner = PlayerService.Instance.Players.Single(player => name.Contains(player.name)).GetComponent<FirstStagePlayerAgent>();

        if (owner.enabled)
            owner.OnDealingDamage(Damage);
    }
}