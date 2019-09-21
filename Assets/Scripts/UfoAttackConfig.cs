using System.Collections.Generic;
using UnityEngine;

public abstract class UfoAttackConfig : ScriptableObject
{
    [SerializeField] private float _windUpDelay;
    [SerializeField] private float _attackDelay;
    [Space]
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private List<BaseBullet> _bullets;

    public float WindUpDelay => _windUpDelay;

    protected float ProjectileSpeed => _projectileSpeed;
    protected List<BaseBullet> Bullets => _bullets;

    public float CreateBullets(Transform target)
    {
        InstantiateBullets(target);
        return _attackDelay;
    }

    protected abstract void InstantiateBullets(Transform target);
}