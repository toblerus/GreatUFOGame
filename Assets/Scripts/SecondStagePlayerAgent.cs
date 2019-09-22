using UnityEngine;

public class SecondStagePlayerAgent : PlayerAgent
{
    protected override void AddEnemyObservations()
    {
        AddVectorObs((Vector2) Container.Instance.AlienBoss.transform.localPosition);
    }
}