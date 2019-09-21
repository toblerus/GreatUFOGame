using UnityEngine;

public class BombBullet : BaseBullet
{
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private float _explosionDelay;
    [SerializeField] private float _explosionDuration;

    public override void DestroyBullet()
    {
        Destroy(gameObject, _explosionDelay);
    }

    protected override bool CanCollide(GameObject collidedObjects)
    {
        var playerHealth = collidedObjects.GetComponent<PlayerHealth>();
        var bullet = collidedObjects.GetComponent<BaseBullet>();
        return playerHealth != null || bullet != null && bullet.BulletType != BulletType.Bomb;
    }

    protected override void OnCollide(GameObject collidedObjects)
    {
        Invoke(nameof(Explode), _explosionDelay);
    }

    private void Explode()
    {
        var explosion = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        Destroy(explosion, _explosionDuration);
    }
}