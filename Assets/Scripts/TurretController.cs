using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _turretSprite;
    [SerializeField] private Transform _bulletSpawnPoint;

    public Transform BulletSpawnPoint => _bulletSpawnPoint;
    
    public void SetTurretSprite(Sprite sprite)
    {
        _turretSprite.sprite = sprite;
    }
}
