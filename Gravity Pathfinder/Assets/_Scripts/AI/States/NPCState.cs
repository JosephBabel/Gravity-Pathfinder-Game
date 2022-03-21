using UnityEngine;
using Utility;

public class NPCState : State<NPC>
{
    private float _startTime;

    protected PlatformNode CurrentPlatform => _data.Navigator.CurrentPlatform;
    protected PlatformNode NextPlatform => _data.Navigator.NextPlatform;
    protected float StateTimeElapsed => Time.time - _startTime;
    protected float DistanceToCurrentPlatform => Vector3.Distance(_data.transform.position, CurrentPlatform.transform.position);
    protected float DistanceToNextPlatform => Vector3.Distance(_data.transform.position, NextPlatform.transform.position);
    protected bool IsOnEdge => MovementUtil.CheckOnEdge(_data.NPCCollider);
    protected bool IsGrounded => MovementUtil.IsGrounded(_data.NPCCollider);

    public NPCState(StateMachine<NPC> stateMachine, NPC data) : base(stateMachine, data) { }

    public override void Enter()
    {
        //Debug.Log($"NPCState: Entering state {GetType()}");
        _startTime = Time.time;
    }

    public override void Exit()
    {
        //Debug.Log($"NPCState: Exiting state {GetType()}");
    }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate() 
    {
        if (NextPlatform && _data.rb.velocity.magnitude > 0)
        {
            Vector3 directionToTarget = _data.Navigator.NextPlatform.transform.position - _data.transform.position;
            Vector3 XZVelocity = new Vector3(_data.rb.velocity.x, 0, _data.rb.velocity.z);
            Vector3 XZDirection = new Vector3(directionToTarget.x, 0, directionToTarget.z);
            if (Vector3.Dot(XZVelocity, XZDirection) < 0)
            {
                MovementUtil.ApplyHorizontalDrag(_data.rb, 3f);
            }
        }
    }
}
