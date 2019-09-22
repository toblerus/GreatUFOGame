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

    private void LateUpdate()
    {
        _p1HealthText.text = "P1 HP: " + PlayerService.Instance.Players[0].PlayerHealth.CurrentHealth;
        _p2HealthText.text = "P2 HP: " + PlayerService.Instance.Players[1].PlayerHealth.CurrentHealth;
        _armorText.text = "Armor: " + PlayerArmor.Instance.CurrentArmor;
        _ufoHealthText.text = "Ufo HP: " + (UfoService.Instance.Ufo.Health.IsDead
                                  ? UfoService.Instance.Ufo.Health.CurrentHealth
                                  : UfoService.Instance.Alien.Health.CurrentHealth);
    }

    private void Update()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(GameController.Instance._timer);
        _timerText.text = string.Format("{0:D2}:{1:D2}.{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        GameController.Instance._timer += Time.deltaTime;
    }
}
