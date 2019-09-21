using UnityEngine;

[CreateAssetMenu(fileName = "UfoMiniGunConfig", menuName = "Config/Ufo Mini-Gun Config")]
public class UfoMiniGunConfig : UfoAttackConfig
{
    protected override void InstantiateBullets(Transform target)
    {
        foreach (var bullet in Bullets)
        {
            var spawnedBullet = Instantiate(bullet);
            spawnedBullet.Speed = ProjectileSpeed;
        }
    }
}