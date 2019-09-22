using UnityEngine;

public static class AttackHelper
{
    public static void RotateTo(this Transform transform, Vector2 direction)
    {
        direction = direction.normalized;
        var rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation - 90);
    }
}