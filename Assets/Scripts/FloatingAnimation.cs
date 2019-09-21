using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _floatingPosition;
    
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _horizontalDistance;
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private float _verticalDistance;

    private void Start()
    {
        _startPosition = transform.localPosition;
    }

    private void FixedUpdate()
    {
        _floatingPosition.x = Mathf.Sin(_horizontalSpeed * Time.time) * _horizontalDistance;
        _floatingPosition.y = Mathf.Sin(_verticalSpeed * Time.time) * _verticalDistance;

        transform.localPosition = _startPosition + _floatingPosition;
    }
}
