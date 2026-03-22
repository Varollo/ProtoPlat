namespace ProtoPlat.Player.Jump.States
{
    public class PlayerJumpGlideState : PlayerJumpState
    {
        public override string AnimationName => "fall";

        public override float GetVelocityY(PlayerFrameData frameData)
        {
            return frameData.Velocity.y * frameData.PlayerData.GlideSpeedMult;
        }
    }
}
