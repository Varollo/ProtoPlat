namespace ProtoPlat.Player.Jump
{
    public abstract class PlayerJumpState : PlayerState
    {
        public virtual float GetVelocityY(PlayerFrameData frameData) => frameData.Velocity.y;
    }
}
