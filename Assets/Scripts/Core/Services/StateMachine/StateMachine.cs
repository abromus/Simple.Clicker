using System;
using System.Collections.Generic;

namespace Clicker.Core
{
    public class StateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IState> _states;

        private IExitState _currentState;

        public StateMachine(GameManagement game, SceneLoader sceneLoader)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this),
                [typeof(GameState)] = new GameState(game),
                [typeof(SceneLoaderState)] = new SceneLoaderState(sceneLoader),
                [typeof(GameLoopState)] = new GameLoopState(game),
            };
        }

        public void Enter<TState>() where TState : class, IEnterState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IEnterState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitState
        {
            _currentState?.Exit();

            var state = GetState<TState>();
            _currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}
