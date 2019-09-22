using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UfoController : MonoBehaviour
{
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

    public Health Health => _health;
    
    private void Start()
    {
        CreateTurrets();
        StartCoroutine(StartShooting());
    }

    private void CreateTurrets()
    {
        foreach (var ufoAttackMapping in _attackMappings)
        {
            var anchor = ufoAttackMapping.Anchor;
            var turretPrefab = Instantiate(_turretPrefab, anchor);
            turretPrefab.transform.SetParent(anchor,true);
            
            var sprite = ufoAttackMapping.Config.GetTurretSprite();
            turretPrefab.SetTurretSprite(sprite);
            turretPrefab.Ufo = transform;

            ufoAttackMapping.Anchor = turretPrefab.BulletSpawnPoint;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator StartShooting()
    {
        while (!_health.IsDead)
        {
            var selectedAttack = _attackMappings[Random.Range(0, _attackMappings.Count)];
            yield return StartCoroutine(selectedAttack.Config.CreateBullets(selectedAttack.Anchor, _health, BulletParent));
            Debug.LogWarning("Bullet creation for " + selectedAttack.Config.name + " completed");
        }
    }
}