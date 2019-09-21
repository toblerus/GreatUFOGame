using UnityEngine;

public class ColorShaderUpdater : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [Space]
    [SerializeField] private Color _redBodyColor;
    [SerializeField] private Color _goldenBodyColor;
    [SerializeField] private Color _goldenOutlineColor;
    [SerializeField] private ColorTint _tint;

    private MaterialPropertyBlock _propBlock;

    private void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetColor("_OutlineColor", _goldenOutlineColor);

        SetPropertyBlock();
    }
 
    private void Update()
    {
        SetPropertyBlock();
    }

    private void SetPropertyBlock()
    {
        // Assign our new value.
        _propBlock.SetColor("_Color", GetBodyColor);
        _propBlock.SetFloat("_OutlineActive", _tint == ColorTint.Golden ? 1 : 0);
        // Apply the edited values to the renderer.
        _renderer.SetPropertyBlock(_propBlock);
    }

    private Color GetBodyColor
    {
        get {
            switch (_tint)
            {
                case ColorTint.Golden:
                    return _goldenBodyColor;
                case ColorTint.Red:
                    return _redBodyColor;
                default:
                    return Color.white;
            }
        }
    }
}
