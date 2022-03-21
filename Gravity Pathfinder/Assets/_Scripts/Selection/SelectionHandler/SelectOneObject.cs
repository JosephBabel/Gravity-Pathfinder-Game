using UnityEngine;

public class SelectOneObject : MonoBehaviour, ISelectionHandler
{
    public void SelectObject(SelectableObject selectedObject)
    {
        selectedObject.Select();

        foreach (var selectableObject in SelectionManager.SelectableObjects)
        {
            if (selectableObject != selectedObject)
            {
                selectableObject.Deselect();
            }
        }
    }
}
