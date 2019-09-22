using System;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _turretSprite;
    [SerializeField] private Transform _bulletSpawnPoint;

    public Transform BulletSpawnPoint => _bulletSpawnPoint;

    public Transform Ufo { private get; set; }
    
    public void SetTurretSprite(Sprite sprite)
    {
        _turretSprite.sprite = sprite;
    }

    private void TargetPlayer()
    {
        var selectedPlayer = PlayerService.Instance.ClosestPlayer(Ufo.position);
        var diff = selectedPlayer.transform.position - transform.position;
        diff.Normalize();

        var rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 90);
    }

    private void Update()
    {
        TargetPlayer();
    }
}
