using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _armorText;
    [SerializeField] private TextMeshProUGUI _p1HealthText;
    [SerializeField] private TextMeshProUGUI _p2HealthText;
    [SerializeField] private TextMeshProUGUI _ufoHealthText;
    [SerializeField] private TextMeshProUGUI _timerText;
    private float _timer;
    
    private void LateUpdate()
    {
        _p1HealthText.text = "P1 HP: " + PlayerService.Instance.Players[0].PlayerHealth.CurrentHealth;
        _p2HealthText.text = "P2 HP: " + PlayerService.Instance.Players[1].PlayerHealth.CurrentHealth;
        _armorText.text = "Armor: " + PlayerArmor.Instance.CurrentArmor;
        _ufoHealthText.text = "Ufo HP: " + UfoService.Instance.Ufo.Health.CurrentHealth;
    }

    private void Update()
    {
        _timerText.text = _timer.ToString("0000");
        _timer += Time.deltaTime;
    }
}
