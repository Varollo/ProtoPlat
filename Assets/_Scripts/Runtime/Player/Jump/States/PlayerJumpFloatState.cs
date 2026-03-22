namespace ProtoPlat.Player.Jump.States
{
    public class PlayerJumpFloatState : PlayerJumpState
    {
        public override string AnimationName => "jump";

        public override float GetVelocityY(PlayerFrameData frameData)
        {
            return frameData.Velocity.y * frameData.PlayerData.FloatSpeedMult;
        }
    }
}
