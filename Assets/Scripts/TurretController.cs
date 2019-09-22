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
    }
}
