public class StateMachine<T>
{
    public State<T> CurrentState { get; private set; }

    public void InitState(State<T> state)
    {
        CurrentState = state;
        CurrentState.Enter();
    }

    public void ChangeState(State<T> state)
    {
        CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }
}
