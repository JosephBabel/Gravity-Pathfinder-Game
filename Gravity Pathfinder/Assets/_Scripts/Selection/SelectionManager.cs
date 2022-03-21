using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static List<SelectableObject> SelectableObjects = new List<SelectableObject>();

    ISelectionHandler _selectionHandler;

    void OnEnable() => EventHandler.OnSelected += HandleSelection;

    void OnDisable() => EventHandler.OnSelected -= HandleSelection;

    void Awake() => _selectionHandler = GetComponent<ISelectionHandler>();

    void HandleSelection(SelectableObject selectedObject)
    {
        _selectionHandler.SelectObject(selectedObject);
    }
}
