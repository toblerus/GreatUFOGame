using System.Collections;
using MLAgents;

public class GameAcademy : Academy
{
    public override void InitializeAcademy()
    {
        foreach (var playerHealth in FindObjectsOfType<PlayerHealth>())
        {
            playerHealth.MaxHealth += PlayerArmor.Instance.MaxArmor;
            playerHealth.CurrentHealth = playerHealth.MaxHealth;
        }

        foreach (var control in FindObjectsOfType<ManualPlayerControl>())
        {
            control.enabled = false;
        }

        PlayerArmor.Instance.MaxArmor = 0;
    }
}
