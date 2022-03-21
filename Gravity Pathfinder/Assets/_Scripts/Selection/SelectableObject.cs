using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SelectableObject : MonoBehaviour
{ 
    public bool IsSelected { get; private set; }

    List<ISelectionResponse> _selectionResponses = new List<ISelectionResponse>();

    void OnEnable() => SelectionManager.SelectableObjects.Add(this);

    void OnDisable() => SelectionManager.SelectableObjects.Remove(this);

    void Awake() => _selectionResponses.AddRange(GetComponents<ISelectionResponse>());

    [ContextMenu("Select Object")]
    public void Select()
    {
        IsSelected = true;

        foreach (var selectionResponse in _selectionResponses)
        {
            selectionResponse.Select();
        }
    }

    [ContextMenu("Deselect Object")]
    public void Deselect()
    {
        IsSelected = false;

        foreach (var selectionResponse in _selectionResponses)
        {
            selectionResponse.Deselect();
        }
    }
}
