using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BaseBullet bullet;
    [SerializeField] private GameObject playerArm;
    [SerializeField] private GameObject playerArmTip;
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private PlayerHealth healthscript;
    public Transform ufoPosition;

    [Header("Player")]
    private float movementSpeed;
    private Vector2 movePosition;
    [SerializeField] private float loweredMovementSpeed = 2.5f;
    [SerializeField] private float standardMovementSpeed = 5f;
    [SerializeField] private float shootDelay = 0.1f;
    [SerializeField] private float projectileSpeed = 5;
    private float time;
    private bool exit;

    [Header("Controls")]
    public float horizontalMovement;
    public float verticalMovement;
    public bool isFiring;

    [Header("Screen Shake")] 
    [SerializeField] private Vector3 _screenShakeStrength;
    [SerializeField] private float _shakeDuration = .2f;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) 
            && GetComponent<ManualPlayerControl>().PlayerIndex == 1)
        {
            healthscript.Damage(1, 0);
        }

        if (healthscript.IsDead)
        {
            return;
        }
        
        Move();
        Shoot();
        RotateTowardsUfo();
    }

    private void Move()
    {
        movementSpeed = isFiring 
            ? loweredMovementSpeed 
            : standardMovementSpeed;
        
        var y = verticalMovement * movementSpeed;
        var x = horizontalMovement * movementSpeed;
        x *= Time.deltaTime;
        y *= Time.deltaTime;
        
        movePosition.x = x;
        movePosition.y = y;
        rigidbody2d.MovePosition(rigidbody2d.position + movePosition);
    }

    private void Shoot()
    {
        if (isFiring && (time > shootDelay))
        {
            time = 0;
            var spawnedBullet = Instantiate(bullet, playerArmTip.transform.position, Quaternion.identity);
            spawnedBullet.MoveTowards(ufoPosition.position - transform.position, projectileSpeed);
            spawnedBullet.name += name;
            ScreenShakeService.Instance.ShakeCamera(_shakeDuration, _screenShakeStrength);
        }
        time += Time.deltaTime;
    }

    private void RotateTowardsUfo()
    {
        var diff = ufoPosition.transform.position - transform.position;
        diff.Normalize();

        var rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        playerArm.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);
    }
/*
    IEnumerator vibrate(PlayerIndex index, float duration)
    {
        GamePad.SetVibration(index, 1f, 1f);

        yield return new WaitForSeconds(duration);
        GamePad.SetVibration(index, 0f, 0f);

        yield return null;
    }
    */
}
