using System.Collections;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _maxSideWayOffset;
    [SerializeField] private BulletType _bulletType;
    [SerializeField] private int _damage;

    public BulletType BulletType => _bulletType;
    protected int Damage => _damage;

    protected bool HasCollided = false;

    public void MoveTowardsGoal(Vector2 goal, float speed)
    {
        StartCoroutine(Move(goal, speed));
    }

    public void MoveTowards(Vector2 direction, float speed)
    {
        direction = direction + new Vector2(Random.Range(-_maxSideWayOffset, _maxSideWayOffset), 0);
        _rigidBody.velocity = direction.normalized * speed;
        transform.RotateTo(direction);
    }

    public abstract void DestroyBullet();

    protected abstract bool CanCollide(GameObject collidedObjects);
    protected abstract void OnCollide(GameObject collidedObjects);

    protected virtual void OnArrivedOnTarget() { }

    private IEnumerator Move(Vector3 goal, float speed)
    {
        var direction = (goal - transform.position).normalized;
        transform.RotateTo(direction);

        while (Vector2.Distance(goal, transform.position) > 0.1f && !HasCollided)
        {
            transform.position += direction * Time.deltaTime * speed;
            yield return null;
        }

        OnArrivedOnTarget();
    }
    
    private void OnBecameInvisible()
    {
        DestroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var bullet = other.GetComponent<BaseBullet>();
        if (bullet != null && bullet.BulletType == _bulletType || HasCollided)
            return;

        if (CanCollide(other.gameObject))
        {
            OnCollide(other.gameObject);
            HasCollided = true;
        }
    }
}