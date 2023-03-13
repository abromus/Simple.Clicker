﻿namespace Clicker.Core.Services
{
    public interface IStateMachine : IService
    {
        public void Enter<TState>() where TState : class, IEnterState;

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IEnterState<TPayload>;
    }
}
