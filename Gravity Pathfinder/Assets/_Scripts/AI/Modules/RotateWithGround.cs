using UnityEngine;
using Utility;

public class RotateWithGround : MonoBehaviour
{
    NPC npc;
    Collider npcCollider;

    private void Awake() 
    {
        npc = GetComponent<NPC>();
        npcCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (MovementUtil.TryGetGround(npcCollider, out RaycastHit hitInfo))
        {
            Vector3 rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal).eulerAngles;
            float zClamped = MathUtil.ClampAngle(rotation.z, -npc.Data.RotateZLimit, npc.Data.RotateZLimit);
            rotation.z = zClamped;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
