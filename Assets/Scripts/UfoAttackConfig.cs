using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UfoAttackConfig : ScriptableObject
{
    [SerializeField] private Sprite _turretSprite;
    [SerializeField] private float _windUpDelay;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _batchDelay;
    [SerializeField] private float _coolDownDelay;
    [Space]
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private int _projectileCount;
    [SerializeField] private int _bulletBatchSize;
    [SerializeField] private List<BaseBullet> _bullets;
    [Space]
    [SerializeField] private GameObject _chargeUpVfx;
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private float _muzzleFlashDuration;

    protected float AttackDelay => _attackDelay;
    protected float BatchDelay => _batchDelay;
    protected float ProjectileSpeed => _projectileSpeed;
    protected int ProjectileCount => _projectileCount;
    protected int BulletBatchSize => _bulletBatchSize;

    protected List<BaseBullet> Bullets => _bullets;

    protected GameObject MuzzleFlash => _muzzleFlash;
    protected float MuzzleFlashDuration => _muzzleFlashDuration;

    public IEnumerator CreateBullets(Transform ufo, Health health, Transform bulletParent)
    {
        if (_chargeUpVfx != null)
            CreateVfx(_chargeUpVfx, Vector2.up, ufo, _windUpDelay);

        yield return new WaitForSeconds(_windUpDelay);
        
        if (health.IsDead)
            yield break;

        var selectedPlayer = PlayerService.Instance.ClosestPlayer(ufo.position);
        yield return health.StartCoroutine(InstantiateBullets(ufo, selectedPlayer.transform, health, bulletParent));
        
        if (health.IsDead)
            yield break;

        yield return new WaitForSeconds(_coolDownDelay);
    }

    protected abstract IEnumerator InstantiateBullets(Transform ufo, Transform target, Health health, Transform bulletParent);

    protected static void CreateVfx(GameObject prefab, Vector2 direction, Transform parent, float lifeTime)
    {
        var instance = Instantiate(prefab, parent);
        instance.transform.RotateTo(direction);

        Destroy(instance, lifeTime);
    }

    public Sprite GetTurretSprite()
    {
        return _turretSprite;
    }
}