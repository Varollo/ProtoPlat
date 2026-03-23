using ProtoPlat.Player.Movement.States;
using ProtoPlat.StateMachines;

namespace ProtoPlat.Player.Movement
{
    public class PlayerMoveStateMachine : AnimatedStateMachine<PlayerMoveState>
    {
        public PlayerMoveStateMachine() : base(
            new PlayerMoveIdleState(),
            new PlayerMoveRunState(),
            new PlayerMoveBreakState(),
            new PlayerMoveTurnState(),
            new PlayerMoveLandState()) { }

        public float GetVelocityX(PlayerFrameData frameData)
        {
            if (CurrentState == null)
                return frameData.Velocity.x;
            return CurrentState.GetVelocityX(frameData);
        }
    }
}
