using System.Collections;
using UnityEngine;

public class BossHealth : Health
{
    [SerializeField] private int _damagePerSecond;

    private void Awake()
    {
        StartCoroutine(DealContinuousDamage());
    }

    private IEnumerator DealContinuousDamage()
    {
        while (!IsDead)
        {
            yield return new WaitForSeconds(1);
            Damage(_damagePerSecond, null);
        }
    }
    
    public override void Heal(int healing)
    {}
}