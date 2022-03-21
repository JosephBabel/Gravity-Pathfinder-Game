using UnityEngine;

public class AccelerationSimulator : MonoBehaviour
{
    [Tooltip("Update acceleration every n frames.")]
    [SerializeField] private int _interval = 5;
    
    public static Vector3 Acceleration;

    GameObject _point;

    void Awake()
    {
        if (Globals.DebugMode)
        {
            _point = new GameObject();
            _point.name = "Acceleration Point";
        }
    }

    void Update()
    {
        if (Globals.DebugMode && Time.frameCount % _interval == 0)
        {
            Acceleration = (_point.transform.position).normalized;
        } 
    }
}
