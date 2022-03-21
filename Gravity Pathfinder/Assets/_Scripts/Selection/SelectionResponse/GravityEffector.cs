using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SelectableObject))]
public class GravityEffector : MonoBehaviour, ISelectionResponse
{
    [Header("Gravity Settings")]
    [SerializeField] float _gravityScale = 1f;

    [Range(0f, 1f)]
    [Tooltip("The limit that gravity can use the z-axis, otherwise default to y-axis.")]
    [SerializeField] float _zGravityLimit = 1f;

    Rigidbody rb;
    bool _isActive;
    float _newGravity;

    public void Select() => _isActive = true;

    public void Deselect() => _isActive = false;

    void OnEnable() 
    { 
        if (Accelerometer.current != null)
        {
            InputSystem.EnableDevice(Accelerometer.current); 
        }
    }

    void OnDisable() 
    { 
        if (Accelerometer.current != null)
        {
            InputSystem.DisableDevice(Accelerometer.current);
        }
    }

    void Awake() => rb = GetComponent<Rigidbody>();

    void Start() => _newGravity = Physics.gravity.magnitude * rb.mass * _gravityScale;

    void FixedUpdate()
    {
        if (_isActive)
        {
            UseControlledGravity();
        }
        else
        {
            UseNormalGravity();
        }
    }

    void UseControlledGravity()
    {
        Vector3 acceleration;

        if (Globals.DebugMode)
        {
            acceleration = AccelerationSimulator.Acceleration;
        }
        else
        {
            acceleration = Accelerometer.current.acceleration.ReadValue();
        }

        float xGravity = acceleration.x * _newGravity;
        float yGravity = acceleration.y * _newGravity;
        float zGravity = acceleration.z * _newGravity;

        if (acceleration.z >= -_zGravityLimit && acceleration.z <= _zGravityLimit)
        {
            rb.AddForce(new Vector3(xGravity, yGravity, -zGravity));
        }
        else
        {
            rb.AddForce(new Vector3(xGravity, zGravity, yGravity));
        }
    }

    void UseNormalGravity()
    {
        rb.AddForce(new Vector3(0f, -_newGravity, 0f));
    }
}
