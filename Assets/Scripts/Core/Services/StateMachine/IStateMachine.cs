namespace Clicker.Core
{
    public interface IStateMachine
    {
        public void Enter<TState>() where TState : class, IEnterState;
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IEnterState<TPayload>;
    }
}
