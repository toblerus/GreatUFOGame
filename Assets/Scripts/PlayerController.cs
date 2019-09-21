using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject playerArm;
    [SerializeField] private GameObject playerArmTip;
    public Transform ufoPosition;

    [Header("Player")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float shootDelay = 0.1f;
    [SerializeField] private float projectileSpeed = 5;
    private float time = 0;
    private bool exit;
    GamePadState state;

    void Start()
    {

    }


    void FixedUpdate()
    {
        Move();
        Shoot();
        ArmFaceUFO();
    }

    private void Move()
    {
            float y = Input.GetAxisRaw("Vertical") * movementSpeed;
            float x = Input.GetAxisRaw("Horizontal") * movementSpeed;
            x *= Time.deltaTime;
            y *= Time.deltaTime;
            transform.Translate(x, y, 0);
    }

    private void Shoot()
    {
        if ((Input.GetAxisRaw("Fire1") == 1) && (time > shootDelay))
        {
            StartCoroutine(vibrate(PlayerIndex.One, 0.05f));
            time = 0;
            var instantiatedProjectile = Instantiate(projectile, playerArmTip.transform.position, Quaternion.identity);
            var projectileScript = instantiatedProjectile.GetComponent<Projectile>();
            projectileScript.UFOPosition = ufoPosition;
            projectileScript.projectileSpeed = projectileSpeed;
        }
        time += Time.deltaTime;
    }

    private void ArmFaceUFO()
    {
        Vector3 diff = ufoPosition.transform.position - transform.position;
        diff.Normalize();

       float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        playerArm.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    IEnumerator vibrate(PlayerIndex index, float duration)
    {
        GamePad.SetVibration(index, 1f, 1f);
        
        yield return new WaitForSeconds(duration);
        GamePad.SetVibration(index, 0f, 0f);

        yield return null;
    }
}
