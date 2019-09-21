using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject playerArm;
    [SerializeField] private GameObject playerArmTip;
    public Transform ufoPosition;

    [Header("Player")]
    private float movementSpeed = 0;
    [SerializeField] private float loweredMovementSpeed = 2.5f;
    [SerializeField] private float standardMovementSpeed = 5f;
    [SerializeField] private float shootDelay = 0.1f;
    [SerializeField] private float projectileSpeed = 5;
    private float time = 0;
    private bool exit;
    [SerializeField] private int playerIndex = 0;
    [SerializeField] private bool isShooting = false;

    void Start()
    {

    }


    void Update()
    {
        Move();
        Shoot();
        ArmFaceUFO();
    }

    private void Move()
    {
        if(isShooting == true)
        {
            movementSpeed = loweredMovementSpeed;
        }
        else
        {
            movementSpeed = standardMovementSpeed;
        }
        string vertical = playerIndex == 1
            ? "Vertical1"
            : "Vertical2";
        string horizontal = playerIndex == 1
            ? "Horizontal1"
            : "Horizontal2";
        float y = Input.GetAxisRaw(vertical) * movementSpeed;
        float x = Input.GetAxisRaw(horizontal) * movementSpeed;
        x *= Time.deltaTime;
        y *= Time.deltaTime;
        transform.Translate(x, y, 0);
    }

    private void Shoot()
    {
        string fire = playerIndex == 1
            ? "Fire1"
            : "Fire2";
        if ((Input.GetButton(fire)) && (time > shootDelay))
        {
            isShooting = true;
            time = 0;
            var instantiatedProjectile = Instantiate(projectile, playerArmTip.transform.position, Quaternion.identity);
            var projectileScript = instantiatedProjectile.GetComponent<Projectile>();
            projectileScript.UFOPosition = ufoPosition;
            projectileScript.projectileSpeed = projectileSpeed;
        }
        time += Time.deltaTime;

        if(Input.GetButtonUp(fire))
        {
            isShooting = false;
        }
    }

    private void ArmFaceUFO()
    {
        Vector3 diff = ufoPosition.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        playerArm.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
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
