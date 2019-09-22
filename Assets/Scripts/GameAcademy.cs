using MLAgents;

public class GameAcademy : Academy
{
    public override void InitializeAcademy()
    {
        foreach (var control in FindObjectsOfType<ManualPlayerControl>())
        {
            control.enabled = false;
        }

        foreach (var agent in FindObjectsOfType<PlayerAgent>())
        {
            agent.enabled = true;
        }
    }
}
