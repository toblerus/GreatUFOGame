using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "UfoBombThrowerConfig", menuName = "Config/Ufo Bomb Thrower Config")]
public class UfoBombThrowerConfig : UfoAttackConfig
{
    [SerializeField] private float _randomOffset;

    protected override IEnumerable InstantiateBullets(Transform ufo, Transform target, Health health)
    {
        foreach (var bullet in Bullets)
        {
            if (health.IsDead)
                yield break;

            var spawnedBullet = Instantiate(bullet, ufo.position, Quaternion.identity);
            var randomOffset = Random.insideUnitCircle * _randomOffset;
            spawnedBullet.MoveTowardsGoal(target.position + new Vector3(randomOffset.x, randomOffset.y, 0), ProjectileSpeed);

            yield return new WaitForSeconds(AttackDelay);
        }
    }
}