using MLAgents;
using MLAgents.CommunicatorObjects;
using UnityEngine;

public abstract class PlayerAgent : Agent
{
    [SerializeField] private PlayerController _playerController;
    
    public override void InitializeAgent()
    {
        Debug.Log("InitializeAgent()");
    }

    public override void CollectObservations()
    {
        var self = _playerController;
        var other = GetComponent<ManualPlayerControl>().PlayerIndex == 1
            ? Container.Instance.Player2
            : Container.Instance.Player1;
        
        AddVectorObs((Vector2) self.transform.localPosition);
        AddVectorObs((Vector2) other.transform.localPosition);

        AddEnemyObservations();
    }

    protected abstract void AddEnemyObservations();

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
