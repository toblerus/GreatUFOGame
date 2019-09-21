using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class UfoController : MonoBehaviour
{
    [Serializable]
    public class AttackAnchorMapping
    {
        public Transform Anchor;
        public UfoAttackType AttackType;
    }

    [SerializeField] private List<UfoAttackConfig> _attacks;
    [SerializeField] private Health _health;
    [SerializeField] private Transform _cannonPosition;
    [SerializeField] private List<AttackAnchorMapping> _anchorMappings;
    
    private void Awake()
    {
        StartCoroutine(StartShooting());
    }

    private IEnumerator StartShooting()
    {
        while (!_health.IsDead)
        {
            var selectedAttack = _attacks[Random.Range(0, _attacks.Count)];

            yield return selectedAttack.CreateBullets(
                _anchorMappings.First(mapping => mapping.AttackType == selectedAttack.AttackType).Anchor,
                _health);
        }
    }
}