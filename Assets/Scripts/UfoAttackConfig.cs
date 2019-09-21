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
    [SerializeField] private List<BaseBullet> _bullets;

    protected float AttackDelay => _attackDelay;
    protected float ProjectileSpeed => _projectileSpeed;

    protected List<BaseBullet> Bullets => _bullets;


    public IEnumerable CreateBullets(Transform ufo, Health health)
    {
        yield return new WaitForSeconds(_windUpDelay);
            
        if (health.IsDead)
            yield break;

        var selectedPlayer = PlayerService.Instance.ClosestPlayer(ufo.position);
        yield return InstantiateBullets(ufo, selectedPlayer.transform, health);
        
        if (health.IsDead)
            yield break;

        yield return _coolDownDelay;
    }

    protected abstract IEnumerable InstantiateBullets(Transform ufo, Transform target, Health health);
}