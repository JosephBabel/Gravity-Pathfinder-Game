using UnityEngine;

[RequireComponent(typeof(FallHandler))]
public class NPCRespawn : MonoBehaviour, IRespawnBehavior<NPC>
{
    Vector3 _spawnPosition;

    void Start() => _spawnPosition = transform.position;

    public void Respawn(NPC npc)
    {
        transform.position = _spawnPosition;
        npc.Navigator.ResetPlatformInfo();
    }
}
