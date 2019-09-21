using UnityEngine;

public abstract class BasePickUp : MonoBehaviour, IPickUp
{
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

    public abstract bool IsCollector(GameObject collider);
    public abstract bool OnCollection(GameObject collider);

    public void Spawn(Vector2 position, float? timeOut)
    {
        transform.position = position;
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
        // Disabling object to return to pool
        IsActive = false;
    }
}