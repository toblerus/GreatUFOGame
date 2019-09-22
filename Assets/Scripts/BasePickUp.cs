using UnityEngine;

public abstract class BasePickUp : MonoBehaviour, IPickUp
{
    private Transform _spawnPoint;

    private bool _isActive = false;
    public bool IsActive
    {
        get => _isActive;
        set
        {
            gameObject.SetActive(value);
            _isActive = value;
        }
    }

    public abstract bool IsCollector(GameObject collidedObject);
    public abstract void OnCollection(GameObject collidedObject);

    public void Spawn(Transform spawnPoint, float? timeOut)
    {
        _spawnPoint = spawnPoint;
        transform.position = spawnPoint.position;
        IsActive = true;

        if (timeOut.HasValue)
        {
            Invoke(nameof(Despawn), timeOut.Value);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsCollector(other.gameObject))
        {
            OnCollection(other.gameObject);
            Despawn();
        }
    }

    private void Despawn()
    {
        // Disabling object & return to pool
        IsActive = false;

        PickUpSystem.Instance.DespawnPickup(_spawnPoint);
        _spawnPoint = null;
    }
}