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
    [SerializeField] private float movementSpeed;
    [SerializeField] private float shootDelay = 0.1f;
    [SerializeField] private float projectileSpeed = 5;
    private float time = 0;
    private bool exit;

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
        if ((Input.GetButton("Fire1")) && (time > shootDelay))
        {
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
}
