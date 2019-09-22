using System.Linq;
using MLAgents;
using MLAgents.CommunicatorObjects;
using UnityEngine;

public abstract class PlayerAgent : Agent
{
    [SerializeField] private PlayerController _playerController;

    private Vector3 _startingPosition;

    public override void InitializeAgent()
    {
        Debug.Log("InitializeAgent()");
        _startingPosition = transform.localPosition;
    }

    public override void CollectObservations()
    {
        var self = _playerController;
        var other = FindObjectsOfType<PlayerController>().Single(player => player.gameObject != gameObject);
        
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

        transform.localPosition = _startingPosition;
    }

    public void OnTakingDamage(int damage)
    {
        AddReward(-damage);
    }

    public void OnDealingDamage(int damage)
    {
        AddReward(10 * damage);
    }

    public void OnDeath()
    {
        AddReward(-100);
        Done();
    }
}
