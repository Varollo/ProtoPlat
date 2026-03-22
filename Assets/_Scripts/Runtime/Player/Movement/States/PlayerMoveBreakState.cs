namespace ProtoPlat.Player.Movement.States
{

    public class PlayerMoveBreakState : PlayerMoveState
    {
        public override string AnimationName => "break";

        public override float GetVelocityX(PlayerFrameData frameData)
        {
            return frameData.Velocity.x * frameData.PlayerData.BreakFriction;
        }
    }
}
