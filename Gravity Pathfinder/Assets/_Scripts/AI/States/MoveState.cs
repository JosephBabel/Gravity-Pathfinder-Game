using UnityEngine;
using Utility;

public class MoveState : NPCState
{
    public MoveState(StateMachine<NPC> stateMachine, NPC data) : base(stateMachine, data) { }

    public override void Enter() => base.Enter();

    public override void Exit()
    { 
        base.Exit();
    }

    public override void LogicUpdate()
    {
        if (NextPlatform && IsOnEdge && IsGrounded)
        {
            Jump();
            _stateMachine.ChangeState(_data.Jump);
        }
        else if (!NextPlatform && CurrentPlatform && IsGrounded && DistanceToCurrentPlatform < 1.8f)
        {
            _stateMachine.ChangeState(_data.Idle);
        }
        else if (!IsGrounded && _data.rb.velocity.y > 0)
        {
            _stateMachine.ChangeState(_data.Jump);
        }
        else if (!IsGrounded && _data.rb.velocity.y < 0)
        {
            _stateMachine.ChangeState(_data.Fall);
        }

        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        if (IsGrounded)
        {
            if (NextPlatform)
            {
                Move();
            }
            else if (CurrentPlatform)
            {
                Return();
            }
        }

        base.PhysicsUpdate();
    }

    void Move()
    {
        Vector3 direction = (NextPlatform.transform.position - _data.transform.position);
        direction = direction.normalized;
        direction.y = 0;
        Debug.DrawRay(_data.transform.position, direction);
        _data.rb.AddRelativeForce(direction * _data.Data.Speed * 2);
        _data.rb.velocity = new Vector3(Mathf.Clamp(_data.rb.velocity.x, -_data.Data.MaxSpeed, _data.Data.MaxSpeed), _data.rb.velocity.y, Mathf.Clamp(_data.rb.velocity.z, -_data.Data.MaxSpeed, _data.Data.MaxSpeed));
    }

    void Return()
    {
        Vector3 direction = (CurrentPlatform.transform.position - _data.transform.position);
        direction = direction.normalized;
        direction.y = 0;
        Debug.DrawRay(_data.transform.position, direction);
        _data.rb.AddRelativeForce(direction * _data.Data.Speed * 2);
        _data.rb.velocity = new Vector3(Mathf.Clamp(_data.rb.velocity.x, -_data.Data.MaxSpeed, _data.Data.MaxSpeed), _data.rb.velocity.y, Mathf.Clamp(_data.rb.velocity.z, -_data.Data.MaxSpeed, _data.Data.MaxSpeed));
    }

    void Jump()
    {
        MovementUtil.JumpToPosition(_data.NPCCollider, NextPlatform.transform.position, _data.Data.MaxJumpForce);
    }
}
