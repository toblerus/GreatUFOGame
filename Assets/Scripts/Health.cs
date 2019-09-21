using System;
using System.Security.Cryptography;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    public int CurrentHealth { get; private set; }

    public bool IsDead { get; private set; } = false;

    public virtual void Damage(int damage)
    {
        if (damage <= 0)
            return;
        
        CurrentHealth = Math.Max(CurrentHealth - damage, 0);
        IsDead = CurrentHealth == 0;
    }

    protected int HealInternal(int healing)
    {
        if (healing <= 0)
            return 0;

        var healthLeft = CurrentHealth + healing;
        CurrentHealth = Math.Min(healthLeft, _maxHealth);

        return Math.Max(healthLeft - _maxHealth, 0);
    }

    public abstract void Heal(int healing);

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }
}
