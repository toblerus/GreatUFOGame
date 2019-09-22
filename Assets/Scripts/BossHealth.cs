using System.Collections;
using UnityEngine;

public class BossHealth : Health
{
    [SerializeField] private int _damagePerSecond;
    [SerializeField] SpriteRenderer[] ufoTextures;
    [SerializeField] Color flashColor;

    private void Start()
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

    public override void Damage(int damage, float? invincibilityTime)
    {
        Debug.Log(damage + " " + IsInvincible);
        base.Damage(damage, invincibilityTime);
        if(damage > 1 && !IsInvincible)
        {
            StartCoroutine(Flash(1));
        }
    }

    IEnumerator Flash(int flashCount)
    {
        for (int i = 0; i < flashCount; i++)
        {
            foreach (var ufoTexture in ufoTextures)
            {
                ufoTexture.color = flashColor;
            }
            yield return new WaitForSeconds(.1f);

            foreach (var ufoTexture in ufoTextures)
            {
                ufoTexture.color = Color.white;
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}