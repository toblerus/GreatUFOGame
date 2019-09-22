using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "UfoRegularAttackConfig", menuName = "Config/Ufo Regular Attack Config")]
public class UfoRegularAttackConfig : UfoAttackConfig
{
    protected override IEnumerator InstantiateBullets(Transform ufo, Transform target, Health health)
    {
        var spawnedBullets = 0;
        while (spawnedBullets < ProjectileCount)
        {
            if (health.IsDead)
                yield break;
            
            var direction = target.position - ufo.position;
            for (var i = 0; i < BulletBatchSize; i++)
            {
                var bulletPrefab = Bullets[Random.Range(0, Bullets.Count)];
                var spawnedBullet = Instantiate(bulletPrefab, ufo.position, Quaternion.identity);
                spawnedBullet.MoveTowards(direction, ProjectileSpeed);

                if (MuzzleFlash != null)
                    CreateVfx(MuzzleFlash, direction, ufo, MuzzleFlashDuration);

                yield return new WaitForSeconds(AttackDelay);
                spawnedBullets++;
            }

            yield return new WaitForSeconds(BatchDelay);
        }
    }
}