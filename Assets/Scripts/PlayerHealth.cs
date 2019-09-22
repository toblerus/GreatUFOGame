using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private PlayerAgent _playerAgent;

    public override void Damage(int damage, float? invincibilityTime)
    {
        if (IsInvincible)
            return;

        var healthDamage = PlayerArmor.Instance.DamageArmor(damage);
        base.Damage(healthDamage, invincibilityTime);
        
        PunishAi(damage);
    }

    public override void Heal(int healing)
    {
        var overHealing = base.HealInternal(healing);
        var overArmoring = PlayerArmor.Instance.HealArmor(overHealing);

        PunishAi(-(healing - overArmoring));
    }
    
    private void PunishAi(int damage)
    {
        if (_playerAgent == null || !_playerAgent.enabled)
            return;

        _playerAgent.OnTakingDamage(damage);

        if (IsDead)
        {
            foreach (var playerAgent in FindObjectsOfType<PlayerAgent>())
            {
                playerAgent.OnDeath();
            }
        }
    }
}