using UnityEngine;

[RequireComponent(typeof(ColorShaderUpdater))]
public class WarningBlinkAnimation : MonoBehaviour
{
    [SerializeField] private float _warningBlinkDuration;
    [SerializeField] private ColorShaderUpdater _shaderUpdater;

    private float _loopTime = 0;
    private Color _offColor;
    private Color _onColor;

    private void Awake()
    {
        _onColor = _shaderUpdater.BodyColor;
        _offColor = new Color(_onColor.r, _onColor.g, _onColor.b, 0);
    }

    private void Update()
    {
        _loopTime += Time.deltaTime;
        _loopTime = _loopTime % (2 * _warningBlinkDuration);

        var color = Color.Lerp(_onColor, _offColor,
            _loopTime > _warningBlinkDuration
                ? 1 - (_loopTime - _warningBlinkDuration) / _warningBlinkDuration
                : _loopTime / _warningBlinkDuration);

        _shaderUpdater.BodyColor = color;
    }

    private void OnValidate()
    {
        if (_shaderUpdater == null)
        {
            _shaderUpdater = GetComponent<ColorShaderUpdater>();
        }
    }
}