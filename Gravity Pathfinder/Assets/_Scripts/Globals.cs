using UnityEngine;

public static class Globals
{
    public static readonly bool DebugMode = true;

    public static readonly int SelectableObjectLayer = LayerMask.GetMask("SelectableObject");
    public static readonly int Ground = LayerMask.GetMask("Ground", "SelectableObject");
    public static readonly float GroundDetectionHeight = 3f;
}
