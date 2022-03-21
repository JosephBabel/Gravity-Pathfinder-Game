using UnityEngine;

public class Highlight : MonoBehaviour, ISelectionResponse
{
    [SerializeField] Material _highlightMaterial;
    Material _defaultMaterial;

    MeshRenderer _meshRenderer;

    public void Select() => _meshRenderer.material = _highlightMaterial;

    public void Deselect() => _meshRenderer.material = _defaultMaterial;

    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultMaterial = _meshRenderer.material;
    }
}
