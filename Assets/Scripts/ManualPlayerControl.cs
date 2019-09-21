using UnityEngine;

public class ManualPlayerControl : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private int playerIndex;

    public int PlayerIndex
    {
        get { return playerIndex; }
    }

    void FixedUpdate()
    {
        playerController.isFiring = Input.GetButton(playerIndex == 1 ? "Fire1" : "Fire2");
        playerController.horizontalMovement = Input.GetAxisRaw(playerIndex == 1 ? "Horizontal1" : "Horizontal2");
        playerController.verticalMovement = Input.GetAxisRaw(playerIndex == 1 ? "Vertical1" : "Vertical2");
    }
}