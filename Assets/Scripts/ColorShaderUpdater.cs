using UnityEngine;

public class ColorShaderUpdater : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [Space]
    [SerializeField] private Color _bodyColor;
    [SerializeField] private Color _outlineColor;
    [SerializeField] private ColorTint _initialTint;

    private MaterialPropertyBlock _propBlock;

    public Color BodyColor
    {
        get => _bodyColor;
        set
        {
            _bodyColor = value;
            _propBlock.SetColor("_Color", GetBodyColor);
        }
    }

    public Color OutlineColor
    {
        get => _outlineColor;
        set
        {
            _outlineColor = value;
            _propBlock.SetFloat("_OutlineActive", _initialTint == ColorTint.OutlineAndBody ? 1 : 0);
        }
    }

    public ColorTint Tint
    {
        set
        {
            _initialTint = value;

            // Assign new propBlock values based on tint.
            _propBlock.SetColor("_Color", GetBodyColor);
            _propBlock.SetFloat("_OutlineActive", _initialTint == ColorTint.OutlineAndBody || _initialTint == ColorTint.OutlineOnly ? 1 : 0);
        }
    }

    private void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetColor("_OutlineColor", _outlineColor);

        Tint = _initialTint;
        SetPropertyBlock();
    }

    private void Update()
    {
        SetPropertyBlock();
    }

    private void SetPropertyBlock()
    {
        // Apply the edited values to the renderer.
        _renderer.SetPropertyBlock(_propBlock);
    }

    private Color GetBodyColor
    {
        get
        {
            switch (_initialTint)
            {
                case ColorTint.OutlineAndBody:
                case ColorTint.BodyOnly:
                    return _bodyColor;
                default:
                    return Color.white;
            }
        }
    }

    private void OnValidate()
    {
        if (_renderer == null)
        {
            _renderer = GetComponent<Renderer>();
        }
    }
}
