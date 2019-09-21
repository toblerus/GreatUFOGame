using UnityEngine;

public class InvincibilityPickUp : BasePickUp
{
    [SerializeField] private float _invincibilityTimeFrame;

    public override bool IsCollector(GameObject collidedObject)
    {
        return collidedObject.GetComponent<PlayerHealth>();
    }

    public override void OnCollection(GameObject collidedObject)
    {
        var playerHealth = collidedObject.GetComponent<PlayerHealth>();
        playerHealth.SetInvincible(_invincibilityTimeFrame);
    }
}