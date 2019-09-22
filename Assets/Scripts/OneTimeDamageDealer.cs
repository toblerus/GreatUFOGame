using System.Collections.Generic;
using UnityEngine;

public class OneTimeDamageDealer : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _invincibilityDuration;

    private readonly List<Health> _damagedEntities = new List<Health>();

    public void Clear()
    {
        _damagedEntities.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var health = other.GetComponent<Health>();
        if (health == null || _damagedEntities.Contains(health))
            return;

        _damagedEntities.Add(health);

        if (health is PlayerHealth)
            health.Damage(_damage, _invincibilityDuration);
        else
            health.Damage(_damage, null);
    }
}