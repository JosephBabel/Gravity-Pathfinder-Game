using UnityEngine;
using Utility;

public class IdleState : NPCState
{
    public IdleState(StateMachine<NPC> stateMachine, NPC data) : base(stateMachine, data) { }

    public override void Enter() => base.Enter();

    public override void Exit() => base.Exit();

    public override void LogicUpdate()
    {
        if (NextPlatform)
        {
            _stateMachine.ChangeState(_data.Move);
        }
        else if (IsGrounded && IsOnEdge)
        {
            _stateMachine.ChangeState(_data.Jump);
        }
        else if (CurrentPlatform && IsGrounded && DistanceToCurrentPlatform >= 1.8f)
        {
            _stateMachine.ChangeState(_data.Move);
        }

        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        MovementUtil.ApplyHorizontalDrag(_data.rb, 3f);

        base.PhysicsUpdate();
    }
}
