using System.Linq;
using MLAgents;
using MLAgents.CommunicatorObjects;
using UnityEngine;

public class PlayerAgent : Agent
{
    [SerializeField] private PlayerController _playerController;
    
    public override void InitializeAgent()
    {
        Debug.Log("InitializeAgent()");
    }

    public override void CollectObservations()
    {
        var players = FindObjectsOfType<PlayerController>();

        var self = players.Single(player => player.gameObject == gameObject);
        var other = players.Single(player => player.gameObject != gameObject);

        AddVectorObs((Vector2) self.transform.localPosition);
        AddVectorObs((Vector2) other.transform.localPosition);
        AddVectorObs((Vector2) FindObjectOfType<Boss>().transform.localPosition);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {

    }

    public override void AgentAction(float[] vectorAction, string textAction, CustomAction customAction)
    {
        _playerController.horizontalMovement = Mathf.Clamp(vectorAction[0], -1, 1);
        _playerController.verticalMovement = Mathf.Clamp(vectorAction[1], -1, 1);
        _playerController.isFiring = vectorAction[2] > 0;
    }

    public override void AgentOnDone()
    {
        
    }

    public override void AgentReset()
    {
        var health = GetComponent<PlayerHealth>();

        health.CurrentHealth = health.MaxHealth;
        health.IsDead = false;
        health.IsInvincible = false;
    }

    public void OnTakingDamage(int damage)
    {
        AddReward(-damage);
    }

    public void OnDealingDamage(int damage)
    {
        AddReward(damage);
    }

    public void OnDeath()
    {
        AddReward(-100);
        Done();
    }
}
