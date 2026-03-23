using ProtoPlat.Input;
using ProtoPlat.Player.Jump.States;
using System;

namespace ProtoPlat.Player.Jump
{
    public class PlayerJumpController : PlayerStateMachineController
    {
        public bool Jump(PlayerFrameData frameData, out float jumpForce)
        {
            jumpForce = 0f;

            if (frameData.IsGrounded)
                jumpForce = frameData.PlayerData.JumpForce;

            return jumpForce != 0f;
        }

        protected override Type GetNextStateType(PlayerFrameData frameData)
        {
            bool isFalling = frameData.Velocity.y < 0;
            bool isRising = frameData.Velocity.y > 0;
            bool jumpInput = frameData.JumpInput;
            
            if (isRising)
                return jumpInput
                    ? typeof(PlayerJumpFloatState)
                    : typeof(PlayerJumpRiseState);

            if (isFalling)
                return jumpInput 
                    ? typeof(PlayerJumpGlideState) 
                    : typeof(PlayerJumpFallState);

            return null;
        }
    }
}
