using System.Linq;
using MLAgents;
using MLAgents.CommunicatorObjects;
using UnityEngine;

public class BossAgent : Agent
{
    public override void InitializeAgent()
    {

    }

    public override void CollectObservations()
    {
        var players = FindObjectsOfType<PlayerController>();
        var player1 = players.Single(player => player.name == "Player1");
        var player2 = players.Single(player => player.name == "Player2");
        
        AddVectorObs(transform.localPosition);
        AddVectorObs(player1.transform.localPosition);
        AddVectorObs(player1.isFiring);
        AddVectorObs(player2.transform.localPosition);
        AddVectorObs(player1.isFiring);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        var boss = GetComponent<Boss>();

        boss.HorizontalMovement = Mathf.Clamp(vectorAction[0], -1, 1);
        boss.VerticalMovement = Mathf.Clamp(vectorAction[1], -1, 1);
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

    public void OnTakingDamage(int damage)
    {
        AddReward(damage);
    }

    public void OnDamageDealt(int damage)
    {
        AddReward(damage);
    }
}
