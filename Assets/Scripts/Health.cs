using System;
using System.Security.Cryptography;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public int MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    [SerializeField] private int _maxHealth;

    public int CurrentHealth { get; set; }

    public bool IsInvincible { get; set; } = false;
    public bool IsDead { get; set; } = false;

    public abstract void Heal(int healing);

    public virtual void Damage(int damage, float? invincibilityTime)
    {
        if (damage <= 0 || IsInvincible)
            return;
        
        CurrentHealth = Math.Max(CurrentHealth - damage, 0);
        IsDead = CurrentHealth == 0;

        if (IsDead || !invincibilityTime.HasValue)
            return;

        SetInvincible(invincibilityTime.Value);
    }

    public void SetInvincible(float timeFrame)
    {
        IsInvincible = true;
        Invoke(nameof(DisableInvincibility), timeFrame);
    }

    protected int HealInternal(int healing)
    {
        if (healing <= 0)
            return 0;

        var healthLeft = CurrentHealth + healing;
        CurrentHealth = Math.Min(healthLeft, _maxHealth);

        return Math.Max(healthLeft - _maxHealth, 0);
    }

    private void DisableInvincibility()
    {
        IsInvincible = false;
    }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }
}
