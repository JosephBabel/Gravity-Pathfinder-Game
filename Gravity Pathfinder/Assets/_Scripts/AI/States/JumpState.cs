using UnityEngine;
using Utility;

public class JumpState : NPCState
{
    public JumpState(StateMachine<NPC> stateMachine, NPC data) : base(stateMachine, data) { }

    public override void Enter() => base.Enter();

    public override void Exit() => base.Exit();

    public override void LogicUpdate()
    {
        if (_data.rb.velocity.y < 0f)
        {
            _stateMachine.ChangeState(_data.Fall);
        }

        base.LogicUpdate();
    }

    public override void PhysicsUpdate() 
    { 
        base.PhysicsUpdate();
    }
}
