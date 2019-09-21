using UnityEngine;

[CreateAssetMenu(fileName = "UfoLaserConfig", menuName = "Config/Ufo Laser Config")]
public class UfoLaserConfig : UfoAttackConfig
{
    [SerializeField] private float _laserLifeTime;

    protected override void InstantiateBullets(Transform target)
    {
        foreach (var bullet in Bullets)
        {
            var spawnedBullet = Instantiate(bullet);
            Destroy(spawnedBullet, _laserLifeTime);
        }
    }
}