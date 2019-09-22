using System.Linq;
using UnityEngine;

public class FirstStagePlayerAgent: PlayerAgent
{
    protected override void AddEnemyObservations()
    {
        var bullets = FindObjectsOfType<BaseBullet>()
            .Where(bullet => bullet.BulletType != BulletType.PlayerBullet)
            .OrderBy(bullet => Vector2.Distance(transform.localPosition, bullet.transform.localPosition))
            .ToList();


        for (var i = 1; i <= 5; i++)
        {
            if (bullets.Count >= i)
            {
                var bullet = bullets[i - 1];
                AddVectorObs(true);
                AddVectorObs((Vector2)bullet.transform.localPosition);
                AddVectorObs(bullet.GetComponent<Rigidbody2D>().velocity);
            }

            else
            {
                AddVectorObs(false);
                AddVectorObs(Vector2.zero);
                AddVectorObs(Vector2.zero);
            }
        }
    }
}