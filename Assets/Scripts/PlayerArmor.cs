using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmor : MonoBehaviour
{
    [SerializeField] private Color _flashColor;
    public static PlayerArmor Instance { get; private set; }

    public int MaxArmor { get { return _maxArmor; } set { _maxArmor = value; } }
    [SerializeField] private int _maxArmor;

    private List<SpriteRenderer> playerSpriteRenderers = new List<SpriteRenderer>();
    
    public int CurrentArmor { get; set; }

    private void Start()
    {
        foreach (var playerController in PlayerService.Instance.Players)
        {
            foreach (var spriteRenderer in playerController.SpritesToFlash)
            {
                playerSpriteRenderers.Add(spriteRenderer);
            }
        }
    }

    public int DamageArmor(int damage)
    {
        if (damage <= 0)
            return 0;
        
        if(damage > 1)
        {
            StartCoroutine(Flash());
        }

        var armorLeft = CurrentArmor - damage;
        CurrentArmor = Math.Max(armorLeft, 0);

        return -1 * Math.Min(armorLeft, 0);
    }

    private IEnumerator Flash()
    {
        foreach (var sprite in playerSpriteRenderers)
        {
            sprite.color = _flashColor;
        }
        yield return new WaitForSeconds(.1f);

        foreach (var sprite in playerSpriteRenderers)
        {
            sprite.color = Color.white;
        }
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