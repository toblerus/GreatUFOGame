using MLAgents;
using MLAgents.CommunicatorObjects;
using UnityEngine;

public class BossAgent : Agent
{
    private readonly Vector2 _player1Vector = new Vector2(1, 1);
    private readonly Vector2 _player2Vector = new Vector2(2, 2);
    private readonly Vector2 _bossVector = new Vector2(3, 3);
    
    public override void InitializeAgent()
    {

    }

    public override void CollectObservations()
    {
        AddVectorObs(_player1Vector);
        AddVectorObs(_player2Vector);
        AddVectorObs(_bossVector);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        Debug.Log(vectorAction[0]);
        Debug.Log(vectorAction[1]);
        Debug.Log(vectorAction[2]);
    }

    public override void AgentAction(float[] vectorAction, string textAction, CustomAction customAction)
    {

    }

    public override void AgentOnDone()
    {
        
    }

    public override void AgentReset()
    {

    }

    public void OnTakingDamage()
    {
        AddReward(-10);
    }

    public void OnDealingDamage()
    {
        AddReward(1);
    }
}
