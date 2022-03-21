using UnityEngine;
using Utility;

public class RotationLimit : MonoBehaviour
{
    [Tooltip("Limit in degrees that this object can rotate by.")]
    [SerializeField] float rotationLimit = 30f;

    void Update() => LimitRotation();

    void LimitRotation()
    {
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.z = MathUtil.ClampAngle(currentRotation.z, -rotationLimit, rotationLimit);
        transform.localEulerAngles = currentRotation;
    }
}
