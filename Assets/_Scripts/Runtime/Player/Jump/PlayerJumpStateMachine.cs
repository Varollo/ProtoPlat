using ProtoPlat.StateMachines;

namespace ProtoPlat.Player.Jump
{
    public class PlayerJumpStateMachine : AnimatedStateMachine<PlayerJumpState>
    {
        public PlayerJumpStateMachine(params PlayerJumpState[] states)
            : base(states) { }

        public float GetVelocityY(PlayerFrameData frameData)
        {
            if (CurrentState == null)
                return frameData.Velocity.x;
            return CurrentState.GetVelocityY(frameData);
        }
    }
}
