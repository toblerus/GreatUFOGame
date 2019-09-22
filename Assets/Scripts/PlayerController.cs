using System;
using System.Collections;
using UnityEngine;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BaseBullet bullet;
    [SerializeField] private GameObject playerArm;
    [SerializeField] private GameObject playerArmTip;
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private PlayerHealth healthscript;
    [SerializeField] private ManualPlayerControl manualPlayerController;

    public PlayerHealth PlayerHealth => healthscript;

    public Transform ufoPosition;

    [Header("Player")]
    private float movementSpeed;
    private Vector2 movePosition;
    [SerializeField] private float loweredMovementSpeed = 2.5f;
    [SerializeField] private float standardMovementSpeed = 5f;
    [SerializeField] private float shootDelay = 0.1f;
    [SerializeField] private float projectileSpeed = 5;
    private Vector3 _spawnPosition;
    private float time;
    private bool exit;

    [Header("Controls")]
    public float horizontalMovement;
    public float verticalMovement;
    public float rsHorizontalMovement;
    public bool isFiring;

    [Header("Screen Shake")]
    [SerializeField] private Vector3 _screenShakeStrength;
    [SerializeField] private float _shakeDuration = .2f;

    private void Start()
    {
        _spawnPosition = transform.position;
    }

    private void Update()
    {
        if (healthscript.IsDead)
        {
            OnDead();
            return;
        }

        Move();
        Shoot();
        Aim();
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
            StartCoroutine(vibrate(manualPlayerController.PlayerIndex, 0.1f));
            time = 0;
            var spawnedBullet = Instantiate(bullet, playerArmTip.transform.position, Quaternion.identity);
            spawnedBullet.MoveTowards(playerArm.transform.up, projectileSpeed);
            spawnedBullet.name += name;
            ScreenShakeService.Instance.ShakeCamera(_shakeDuration, _screenShakeStrength);
        }
        /*
        else
        {
            if(!isFiring)
            {
            time = shootDelay;
            Debug.Log("called");

            }
        }
        */
        time += Time.deltaTime;
    }
    /*
    private void RotateTowardsUfo()
    {
        var diff = ufoPosition.transform.position - transform.position;
        diff.Normalize();

        var rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        playerArm.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);
    }
    */

    private void Aim()
    {
        playerArm.transform.eulerAngles = new Vector3(
        playerArm.transform.eulerAngles.x,
        playerArm.transform.eulerAngles.y,
        -rsHorizontalMovement*45
        );
    }

    private void OnDead()
    {
        transform.position = new Vector3(-100, 0, 0);
    }

    public void ResetPlayer()
    {
        transform.position = _spawnPosition;
        healthscript.CurrentHealth = healthscript.MaxHealth;
    }

    IEnumerator vibrate(int index, float duration)
    {
        GamePad.SetVibration(index == 1 ? PlayerIndex.One : PlayerIndex.Two, 1f, 1f);

        yield return new WaitForSeconds(duration);
        GamePad.SetVibration(index == 1 ? PlayerIndex.One : PlayerIndex.Two, 0f, 0f);

        yield return null;
    }
}
