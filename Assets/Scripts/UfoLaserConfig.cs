using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "UfoLaserConfig", menuName = "Config/Ufo Laser Config")]
public class UfoLaserConfig : UfoAttackConfig
{
    [SerializeField] private float _laserLifeTime;

    protected override IEnumerable InstantiateBullets(Transform ufo, Transform target, Health health)
    {
        foreach (var bullet in Bullets)
        {
            if (health.IsDead)
                yield break;

            var spawnedBullet = Instantiate(bullet, ufo.position, Quaternion.identity);
            Destroy(spawnedBullet, _laserLifeTime);

            yield return new WaitForSeconds(AttackDelay);
        }
    }
}