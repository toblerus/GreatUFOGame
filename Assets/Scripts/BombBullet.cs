using UnityEngine;

public class BombBullet : BaseBullet
{
    [SerializeField] private GameObject _warningEffect;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _bombLifeTime;
    [Space]
    [SerializeField] private float _explosionDelay;
    [SerializeField] private float _explosionDuration;
    [SerializeField] private GameObject _explosionEffect;

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
        _warningEffect.SetActive(true);
        _collider.enabled = false;
    }

    protected override void OnArrivedOnTarget()
    {
        Invoke(nameof(TryExploding), _bombLifeTime);
    }

    private void TryExploding()
    {
        if (HasCollided)
            return;

        OnCollide(null);
        Invoke(nameof(DestroyBullet), _explosionDelay);
        HasCollided = true;
    }

    private void Explode()
    {
        var explosion = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        Destroy(explosion, _explosionDuration);
    }
}