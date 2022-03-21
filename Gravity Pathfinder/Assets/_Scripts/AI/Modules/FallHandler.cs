using UnityEngine;

[RequireComponent(typeof(NPC), typeof(IRespawnBehavior<NPC>))]
public class FallHandler : MonoBehaviour
{
    [Tooltip("Y value to reset this gameobject's position.")]
    [SerializeField] float _yResetValue = -18f;

    IRespawnBehavior<NPC> _respawnBehavior;

    NPC npc;

    void Awake()
    {
        npc = GetComponent<NPC>();
        _respawnBehavior = GetComponent<IRespawnBehavior<NPC>>();
    }

    void Update()
    {
        if (transform.position.y < _yResetValue)
        {
            _respawnBehavior.Respawn(npc);
        }
    }
}
