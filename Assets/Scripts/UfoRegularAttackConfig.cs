﻿using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "UfoRegularAttackConfig", menuName = "Config/Ufo Regular Attack Config")]
public class UfoRegularAttackConfig : UfoAttackConfig
{
    protected override IEnumerator InstantiateBullets(Transform ufo, Transform target, Health health)
    {
        for (var i = 0; i < ProjectileCount; i++)
        {
            if (health.IsDead)
                yield break;

            var bulletPrefab = Bullets[Random.Range(0, Bullets.Count)];

            var spawnedBullet = Instantiate(bulletPrefab, ufo.position, Quaternion.identity);
            spawnedBullet.MoveTowards(target.position - ufo.position, ProjectileSpeed);

            yield return new WaitForSeconds(AttackDelay);
        }
    }
}