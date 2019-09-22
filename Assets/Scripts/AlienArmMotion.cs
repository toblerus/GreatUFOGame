using System.Collections;
using UnityEngine;

public class AlienArmMotion : MonoBehaviour
{
    [SerializeField] private Transform _tileableObject;
    [SerializeField] private SpriteRenderer _tileableSpriteRenderer;
    [SerializeField] private Transform _spikeObject;
    [Space]
    [SerializeField] private Transform _rotatedObject;
    [SerializeField] private Vector2 _defaultLookDirection;
    [SerializeField] private float _rotationSpeed;

    private bool _isRotating = true;
    private Vector2 _currentLookDirection;

    public Transform Target { set; private get; }
    public Vector3 OriginalTilePosition => _tileableObject.position;

    public IEnumerator ModifyArm(Vector3 targetPosition, float animationDuration, bool allowRotation)
    {
        if (!allowRotation)
        {
            _isRotating = false;
        }
        
        var direction = (targetPosition - _tileableObject.position).normalized;
        var speed = Vector2.Distance(targetPosition, _tileableObject.position) / animationDuration;
        
        while (Vector2.Distance(_tileableObject.position, targetPosition) > 0.1)
        {
            var movement = direction * speed * Time.deltaTime;
            
            _spikeObject.position += 2.0f * movement;
            _tileableObject.position += movement;

            _tileableSpriteRenderer.size = new Vector2(1, 2.0f * _tileableObject.localPosition.y);

            yield return null;   
        }

        if (allowRotation)
        {
            _isRotating = true;
        }
    }
    
    private void Update()
    {
        if (!_isRotating)
            return;

        var direction = _defaultLookDirection;
        if (Target != null)
        {
            direction = Target.position - _rotatedObject.position;
        }

        _currentLookDirection = Vector2.Lerp(_currentLookDirection, direction, Time.deltaTime * 360 / _rotationSpeed);
        _rotatedObject.RotateTo(_currentLookDirection);
    }

    private void Awake()
    {
        _currentLookDirection = _defaultLookDirection;
    }
}
