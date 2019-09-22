using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UfoController : MonoBehaviour
{
    [SerializeField] private float _initialShootingDelay;
    
    [Serializable]
    public class UfoAttackMapping
    {
        public UfoAttackConfig Config;
        public Transform Anchor;
    }

    public Transform BulletParent;
    [SerializeField] private TurretController _turretPrefab;
    [SerializeField] private Health _health;
    [SerializeField] private List<UfoAttackMapping> _attackMappings;

    private Coroutine _shooting;

    public Health Health => _health;
    
    private void Start()
    {
        CreateTurrets();
    }

    private void CreateTurrets()
    {
        if (_turretPrefab == null)
            return;

        foreach (var ufoAttackMapping in _attackMappings)
        {
            var anchor = ufoAttackMapping.Anchor;
            var turretPrefab = Instantiate(_turretPrefab, anchor);
            turretPrefab.transform.SetParent(anchor,true);
            
            var sprite = ufoAttackMapping.Config.GetTurretSprite();
            turretPrefab.SetTurretSprite(sprite);

            ufoAttackMapping.Anchor = turretPrefab.BulletSpawnPoint;
        }
    }

    private void OnEnable()
    {
        _shooting = StartCoroutine(StartShooting());
    }

    private void OnDisable()
    {
        StopCoroutine(_shooting);
    }

    private IEnumerator StartShooting()
    {
        while (!_health.IsDead)
        {
            yield return new WaitForSeconds(_initialShootingDelay);
            
            var selectedAttack = _attackMappings[Random.Range(0, _attackMappings.Count)];
            yield return StartCoroutine(selectedAttack.Config.CreateBullets(selectedAttack.Anchor, _health, BulletParent));
            Debug.LogWarning("Bullet creation for " + selectedAttack.Config.name + " completed");
        }
    }
}