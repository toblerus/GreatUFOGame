using UnityEngine;

public class FirstStagePlayerAgent: PlayerAgent
{
    protected override void AddEnemyObservations()
    {
        AddVectorObs((Vector2)Container.Instance.Ufo.transform.localPosition);
    }
}