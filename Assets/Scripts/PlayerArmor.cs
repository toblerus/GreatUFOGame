using System;
using UnityEngine;

public class PlayerArmor : MonoBehaviour
{
    public static PlayerArmor Instance { get; private set; }

    public int MaxArmor { get { return _maxArmor; } set { _maxArmor = value; } }
    [SerializeField] private int _maxArmor;

    public int CurrentArmor { get; private set; }
    
    public int DamageArmor(int damage)
    {
        if (damage <= 0)
            return 0;

        var armorLeft = CurrentArmor - damage;
        CurrentArmor = Math.Max(armorLeft, 0);

        return -1 * Math.Min(armorLeft, 0);
    }

    public int HealArmor(int healing)
    {
        if (healing < 0)
            return 0;

        var missingArmor = _maxArmor - CurrentArmor;
        var overHeal = Math.Max(healing - missingArmor, 0);
        CurrentArmor += healing - overHeal;

        return overHeal;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        CurrentArmor = _maxArmor;
    }
}