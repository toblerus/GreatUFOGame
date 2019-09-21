using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "UfoMiniGunConfig", menuName = "Config/Ufo Mini-Gun Config")]
public class UfoMiniGunConfig : UfoAttackConfig
{
    protected override IEnumerable InstantiateBullets(Transform ufo, Transform target, Health health)
    {
        foreach (var bullet in Bullets)
        {
            if (health.IsDead)
                yield break;

            var spawnedBullet = Instantiate(bullet, ufo.position, Quaternion.identity);
            spawnedBullet.Speed = ProjectileSpeed;

            yield return new WaitForSeconds(AttackDelay);
        }
    }
}