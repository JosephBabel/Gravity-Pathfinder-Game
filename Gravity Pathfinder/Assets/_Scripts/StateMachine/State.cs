public abstract class State<T>
{
    protected StateMachine<T> _stateMachine;
    protected T _data;

    public State(StateMachine<T> stateMachine, T data)
    {
        _stateMachine = stateMachine;
        _data = data;
    }

    public abstract void Enter();

    public abstract void Exit();

    public abstract void LogicUpdate();

    public abstract void PhysicsUpdate();
}
