using DG.Tweening;
using UnityEngine;

public class ScreenShakeService : MonoBehaviour
{
    public static ScreenShakeService Instance { get; private set; }

    private Transform _cameraTransform;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _cameraTransform = Camera.main.transform;
    }

    public void ShakeCamera(float duration, Vector3 strength)
    {
        _cameraTransform.DOShakePosition(
            duration,
            strength);
    }
}
