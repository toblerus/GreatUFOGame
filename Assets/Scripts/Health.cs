using System;
using System.Collections;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _spriteRenderers;
    
    [SerializeField] private Color _flashColor;
    
    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    [SerializeField] private int _maxHealth;
    [SerializeField] private ColorShaderUpdater _colorShader;

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

        if(damage > 1 && !IsInvincible)
        {
            StartCoroutine(Flash());
        }
        
        if (IsDead || !invincibilityTime.HasValue)
            return;

        SetInvincible(invincibilityTime.Value);
    }

    public void SetInvincible(float timeFrame)
    {
        IsInvincible = true;
        Invoke(nameof(DisableInvincibility), timeFrame);

        UpdateColorShader();
    }
    
    private IEnumerator Flash()
    {
        foreach (var sprite in _spriteRenderers)
        {
            sprite.color = _flashColor;
        }
        yield return new WaitForSeconds(.1f);

        foreach (var sprite in _spriteRenderers)
        {
            sprite.color = Color.white;
        }
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
        UpdateColorShader();
    }

    private void UpdateColorShader()
    {
        if (_colorShader == null)
            return;

        _colorShader.Tint = IsInvincible ? ColorTint.OutlineOnly : ColorTint.None;
    }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }
}
