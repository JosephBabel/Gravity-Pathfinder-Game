static class EventHandler
{
    public delegate void Selected(SelectableObject selectableObject);
    public static event Selected OnSelected;

    public static void SelectObject(SelectableObject selectableObject) => OnSelected(selectableObject);
}