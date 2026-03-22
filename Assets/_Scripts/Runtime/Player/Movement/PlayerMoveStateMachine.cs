using ProtoPlat.StateMachines;

namespace ProtoPlat.Player.Movement
{
    public class PlayerMoveStateMachine : AnimatedStateMachine<PlayerMoveState>
    {
        public PlayerMoveStateMachine(params PlayerMoveState[] states) 
            : base(states) { }

        public float GetVelocityX(PlayerFrameData frameData)
        {
            if (CurrentState == null)
                return frameData.Velocity.x;
            return CurrentState.GetVelocityX(frameData);
        }
    }
}
