using UnityEngine;

namespace ProtoPlat.Player.Movement
{
    public abstract class PlayerMoveState : PlayerState
    {
        public abstract float GetVelocityX(PlayerFrameData frameData);
    }
}
