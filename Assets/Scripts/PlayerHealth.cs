public class PlayerHealth : Health
{
    public override void Damage(int damage)
    {
        var healthDamage = PlayerArmor.Instance.DamageArmor(damage);
        base.Damage(healthDamage);
    }

    public override void Heal(int healing)
    {
        var overHealing = base.HealInternal(healing);
        PlayerArmor.Instance.HealArmor(overHealing);
    }
}