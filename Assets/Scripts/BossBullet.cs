using UnityEngine;

public class BossBullet : BaseBullet
{
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

        var bullet = collidedObjects.GetComponent<BaseBullet>();
        if (bullet)
        {
            bullet.DestroyBullet();
        }
    }
}