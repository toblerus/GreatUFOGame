using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float movementSpeed;

    void Start()
    {
        
    }


    void Update()
    {
        float y = Input.GetAxis("Vertical") * movementSpeed;
        float x = Input.GetAxis("Horizontal") * movementSpeed;
        x *= Time.deltaTime;
        y *= Time.deltaTime;
        transform.Translate(x, y, 0);
    }
}
