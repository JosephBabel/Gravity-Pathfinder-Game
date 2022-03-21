using UnityEngine;

public class FallState : NPCState
{
    public FallState(StateMachine<NPC> stateMachine, NPC data) : base(stateMachine, data) { }

    public override void Enter() => base.Enter();

    public override void Exit() => base.Exit();

    public override void LogicUpdate()
    {
        if (IsGrounded)
        {
            _stateMachine.ChangeState(_data.Move);
        }

        base.LogicUpdate();
    }

    public override void PhysicsUpdate() 
    { 
        base.PhysicsUpdate();
    }
}
