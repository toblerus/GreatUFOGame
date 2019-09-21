using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UfoAttackConfig : ScriptableObject
{
    [SerializeField] private float _windUpDelay;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _coolDownDelay;
    [Space]
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private int _projectileCount;
    [SerializeField] private List<BaseBullet> _bullets;

    protected float AttackDelay => _attackDelay;
    protected float ProjectileSpeed => _projectileSpeed;
    protected float ProjectileCount => _projectileCount;

    protected List<BaseBullet> Bullets => _bullets;


    public IEnumerator CreateBullets(Transform ufo, Health health)
    {
        yield return new WaitForSeconds(_windUpDelay);
            
        if (health.IsDead)
            yield break;

        var selectedPlayer = PlayerService.Instance.ClosestPlayer(ufo.position);
        yield return health.StartCoroutine(InstantiateBullets(ufo, selectedPlayer.transform, health));
        
        if (health.IsDead)
            yield break;

        yield return new WaitForSeconds(_coolDownDelay);
    }

    protected abstract IEnumerator InstantiateBullets(Transform ufo, Transform target, Health health);
}