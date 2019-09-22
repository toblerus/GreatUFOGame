using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "UfoBombThrowerConfig", menuName = "Config/Ufo Bomb Thrower Config")]
public class UfoBombThrowerConfig : UfoAttackConfig
{
    [SerializeField] private float _randomOffset;

    protected override IEnumerator InstantiateBullets(Transform ufo, Transform target, Health health, Transform bulletParent)
    {
        for (var i = 0; i < ProjectileCount; i++)
        {
            if (health.IsDead)
                yield break;

            var bulletPrefab = Bullets[Random.Range(0, Bullets.Count)];
            
            var spawnedBullet = Instantiate(bulletPrefab, ufo.position, Quaternion.identity, bulletParent);
            var randomOffset = Random.insideUnitCircle * _randomOffset;
            spawnedBullet.MoveTowardsGoal(target.position + new Vector3(randomOffset.x, randomOffset.y, 0), ProjectileSpeed);

            yield return new WaitForSeconds(AttackDelay);
        }
    }
}