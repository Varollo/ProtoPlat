using UnityEngine;

namespace ProtoPlat.Player.Movement.States
{
    public class PlayerMoveRunState : PlayerMoveState
    {
        public override string AnimationName => "run";

        public override float GetVelocityX(PlayerFrameData frameData)
        {
            return Mathf.MoveTowards(frameData.Velocity.x, 
                frameData.MoveInput * frameData.PlayerData.MaxSpeed, 
                frameData.PlayerData.AccRate);
        }
    }
}
