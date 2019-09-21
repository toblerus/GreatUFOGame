using UnityEngine;

[CreateAssetMenu(fileName = "UfoBombThrowerConfig", menuName = "Config/Ufo Bomb Thrower Config")]
public class UfoBombThrowerConfig : UfoAttackConfig
{
    [SerializeField] private float _randomOffset;

    protected override void InstantiateBullets(Transform target)
    {
        foreach (var bullet in Bullets)
        {
            var spawnedBullet = Instantiate(bullet);
            var randomOffset = Random.insideUnitCircle * _randomOffset;
            spawnedBullet.MoveTowardsGoal(target.position + new Vector3(randomOffset.x, randomOffset.y, 0), ProjectileSpeed);
        }
    }
}