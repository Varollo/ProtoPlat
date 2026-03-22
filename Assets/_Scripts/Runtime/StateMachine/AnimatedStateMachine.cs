using ProtoPlat.StateMachines;

namespace ProtoPlat.StateMachines
{
    public class AnimatedStateMachine<TState> : StateMachine<TState> where TState : IAnimatedState
    {
        public string CurrentAnimationName => CurrentState.AnimationName;

        public AnimatedStateMachine(params TState[] states) 
            : base(states) { }
    }
}
