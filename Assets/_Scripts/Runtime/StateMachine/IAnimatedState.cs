using ProtoPlat.StateMachines;

namespace ProtoPlat.StateMachines
{
    public interface IAnimatedState : IState
    {
        string AnimationName { get; }
    }
}
