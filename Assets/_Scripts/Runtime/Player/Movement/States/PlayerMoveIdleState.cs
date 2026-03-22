using UnityEngine;

namespace ProtoPlat.Player.Movement.States
{
    public class PlayerMoveIdleState : PlayerMoveState
    {
        public override string AnimationName => "idle";

        public override float GetVelocityX(PlayerFrameData frameData)
        {
            return 0f;
        }
    }
}
