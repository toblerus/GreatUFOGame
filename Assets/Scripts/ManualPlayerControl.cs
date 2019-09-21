using UnityEngine;

public class ManualPlayerControl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController playerController;

    [Header("Controls")]
    [SerializeField] private string fireButton;
    [SerializeField] private string horizontalAxis;
    [SerializeField] private string verticalAxis;

    void FixedUpdate()
    {
        playerController.isFiring = Input.GetButton(fireButton);
        playerController.horizontalMovement = Input.GetAxisRaw(horizontalAxis);
        playerController.verticalMovement = Input.GetAxisRaw(verticalAxis);
    }
}