using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "Data/NPCData")]
public class NPCData : ScriptableObject
{
    [SerializeField] float speed = 6f;
    public float Speed { get { return speed; } }

    [SerializeField] float maxSpeed = 10f;
    public float MaxSpeed { get { return maxSpeed; } }

    [SerializeField] float maxJumpForce = 15f;
    public float MaxJumpForce { get { return maxJumpForce; } }

    [SerializeField] float rotateZLimit = 20f;
    public float RotateZLimit { get { return rotateZLimit; } }
}
