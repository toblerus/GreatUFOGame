using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    
    [SerializeField] private Health _health;
    [SerializeField] private List<UfoAttackMapping> _attackMappings;
    
    private void Awake()
    {
        StartCoroutine(StartShooting());
    }

    private IEnumerator StartShooting()
    {
        while (!_health.IsDead)
        {
            var selectedAttack = _attackMappings[Random.Range(0, _attackMappings.Count)];

            yield return selectedAttack.Config.CreateBullets(selectedAttack.Anchor, _health);
        }
    }
}