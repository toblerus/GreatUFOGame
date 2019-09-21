using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "UfoLaserConfig", menuName = "Config/Ufo Laser Config")]
public class UfoLaserConfig : UfoAttackConfig
{
    [SerializeField] private float _laserLifeTime;

    protected override IEnumerator InstantiateBullets(Transform ufo, Transform target, Health health)
    {
        for (var i = 0; i < ProjectileCount; i++)
        {
            if (health.IsDead)
                yield break;
            
            var bulletPrefab = Bullets[Random.Range(0, Bullets.Count)];

            var spawnedBullet = Instantiate(bulletPrefab, ufo.position, Quaternion.identity);
            spawnedBullet.MoveTowards(target.position - ufo.position, ProjectileSpeed);
            Destroy(spawnedBullet, _laserLifeTime);

            yield return new WaitForSeconds(AttackDelay);
        }
    }
}