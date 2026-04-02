using System;
using System.Collections.Generic;
using System.Linq;

namespace ProtoPlat.StateMachines
{
    public abstract class StateMachine<TState> where TState : IState
    {
        public event Action<Type> OnStateChange;

        private readonly Dictionary<Type, TState> _states = new();

        protected StateMachine(params TState[] states)
        {
            _states = states.ToDictionary(state => state.GetType());
        }

        public TState CurrentState { get; private set; }

        public void Transition<T>() => Transition(typeof(T));
        public void Transition(Type nextStateType)
        {
            var oldState = CurrentState;

            if (oldState != null && !oldState.CanExit())
                return;

            if (!_states.TryGetValue(nextStateType, out var newState))
                return;

            if (!newState.CanEnter())
                return;

            oldState?.ExitState();
            newState.EnterState();
            
            CurrentState = newState;
            OnTransition(oldState, newState);
            OnStateChange?.Invoke(nextStateType);
        }

        protected virtual void OnTransition(TState prevState, TState nextState) { }
    }
}
