public class PlayerHealth : Health
{
    public override void Damage(int damage, float? invincibilityTime)
    {
        if (IsInvincible)
            return;

        var healthDamage = PlayerArmor.Instance.DamageArmor(damage);
        base.Damage(healthDamage, invincibilityTime);
    }

    public override void Heal(int healing)
    {
        var overHealing = base.HealInternal(healing);
        PlayerArmor.Instance.HealArmor(overHealing);
    }
}