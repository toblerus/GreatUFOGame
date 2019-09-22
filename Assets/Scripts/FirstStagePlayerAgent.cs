using System.Linq;
using UnityEngine;

public class FirstStagePlayerAgent: PlayerAgent
{
    protected override void AddEnemyObservations()
    {
        var bullets = Container.Instance.Ufo.BulletParent
            .Cast<Transform>()
            .Select(child => child.GetComponent<BaseBullet>())
            .ToList();
        
        for (var i = 1; i <= 30; i++)
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