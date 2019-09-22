using UnityEngine;

public class LaserBullet : BaseBullet
{
    [SerializeField] private GameObject _lineRenderer;
    [SerializeField] private float _additionalLineLifetime;
    [Space]
    [SerializeField] private float _invincibilityDuration;

    public override void DestroyBullet()
    {
        _lineRenderer.transform.SetParent(null, true);
        Destroy(_lineRenderer, _additionalLineLifetime);
        Destroy(gameObject);
    }

    protected override bool CanCollide(GameObject collidedObjects)
    {
        return collidedObjects.GetComponent<PlayerHealth>() != null;
    }

    protected override void OnCollide(GameObject collidedObjects)
    {
        var playerHealth = collidedObjects.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Damage(Damage, _invincibilityDuration);
        }
    }
}