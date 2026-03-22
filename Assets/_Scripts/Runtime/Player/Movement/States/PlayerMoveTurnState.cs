namespace ProtoPlat.Player.Movement.States
{
    public class PlayerMoveTurnState : PlayerMoveState
    {
        public override string AnimationName => "turn";

        public override float GetVelocityX(PlayerFrameData frameData)
        {
            return frameData.Velocity.x * frameData.PlayerData.TurnFriction;
        }
    }
}
