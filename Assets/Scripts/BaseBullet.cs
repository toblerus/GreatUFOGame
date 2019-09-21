﻿using System.Collections;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private Vector2 _localShotDirection;
    [SerializeField] private BulletType _bulletType;
    [SerializeField] private int _damage;

    private bool _hasCollided = false;

    public BulletType BulletType => _bulletType;
    protected int Damage => _damage;

    public float Speed
    {
        set
        {
            var moveDirection = transform.TransformDirection(_localShotDirection);
            _rigidBody.velocity = moveDirection * value;
            transform.LookAt(moveDirection);
        }
    }

    public void MoveTowardsGoal(Vector2 goal, float speed)
    {
        StartCoroutine(MoveTowards(goal, speed));
    }

    public abstract void DestroyBullet();

    protected abstract bool CanCollide(GameObject collidedObjects);
    protected abstract void OnCollide(GameObject collidedObjects);

    private IEnumerator MoveTowards(Vector3 goal, float speed)
    {
        var direction = (goal - transform.position).normalized;
        transform.LookAt(direction);

        while (Vector2.Distance(goal, transform.position) > 0.1f)
        {
            transform.position += direction * Time.deltaTime * speed;
            yield return null;
        }
    }

    private void OnBecameInvisible()
    {
        DestroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var bullet = other.GetComponent<BaseBullet>();
        if (bullet != null && bullet.BulletType == _bulletType || _hasCollided)
            return;

        if (CanCollide(other.gameObject))
        {
            OnCollide(other.gameObject);
            _hasCollided = true;
        }
    }
}