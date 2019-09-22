using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private void TargetPlayer()
    {
        var selectedPlayer = PlayerService.Instance.ClosestPlayer(transform.position);
        var diff = selectedPlayer.transform.position - transform.position;
        diff.Normalize();

        var rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 90);
    }

    private void Update()
    {
        TargetPlayer();
    }
}
