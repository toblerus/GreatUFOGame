using System;
using UnityEngine;

public class PlayerArmor : MonoBehaviour
{
    public static PlayerArmor Instance { get; private set; }

    [SerializeField] private int _maxArmor;

    public int CurrentArmor { get; private set; }
    
    public int DamageArmor(int damage)
    {
        if (damage <= 0)
            return 0;

        var armorLeft = CurrentArmor - damage;
        CurrentArmor = Math.Max(armorLeft, 0);

        return Math.Min(armorLeft, 0);
    }

    public void HealArmor(int healing)
    {
        if (healing < 0)
            return;

        CurrentArmor = Math.Min(CurrentArmor + healing, _maxArmor);
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