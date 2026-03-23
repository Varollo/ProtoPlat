using ProtoPlat.Player.Jump.States;
using ProtoPlat.StateMachines;

namespace ProtoPlat.Player.Jump
{
    public class PlayerJumpStateMachine : AnimatedStateMachine<PlayerJumpState>
    {
        public PlayerJumpStateMachine() : base(
            new PlayerJumpFallState(),
            new PlayerJumpRiseState(),
            new PlayerJumpGlideState(),
            new PlayerJumpFloatState()) { }

        public float GetVelocityY(PlayerFrameData frameData)
        {
            if (CurrentState == null)
                return frameData.Velocity.x;
            return CurrentState.GetVelocityY(frameData);
        }
    }
}
