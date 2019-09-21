using UnityEngine;

public class BossBullet : BaseBullet
{
    [SerializeField] private float _invincibilityDuration;

    public override void DestroyBullet()
    {
        Destroy(gameObject);
    }

    protected override bool CanCollide(GameObject collidedObjects)
    {
        var playerHealth = collidedObjects.GetComponent<PlayerHealth>();
        var bullet = collidedObjects.GetComponent<BaseBullet>();
        return playerHealth != null || bullet != null && bullet.BulletType == BulletType.Bomb;
    }

    protected override void OnCollide(GameObject collidedObjects)
    {
        var playerHealth = collidedObjects.GetComponent<PlayerHealth>();
        if (playerHealth)
        {
            playerHealth.Damage(Damage, _invincibilityDuration);
        }
    }
}