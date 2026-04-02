using ProtoPlat.Player.Movement.States;
using System;
using UnityEngine;

namespace ProtoPlat.Player.Movement
{
    public class PlayerMoveController : PlayerStateMachineController
    {
        protected override Type GetNextStateType(PlayerFrameData frameData)
        {
            bool hasInput = frameData.MoveInput != 0;
            bool isMoving = Mathf.Abs(frameData.Velocity.x) > 1f;
            bool isTurning = hasInput && isMoving && Mathf.Sign(frameData.MoveInput) != Mathf.Sign(frameData.Velocity.x);
            bool isLanding = frameData.LateVelocity.y < -0.1f && (Mathf.Approximately(frameData.Velocity.y, 0) || frameData.IsGrounded);

            if (isLanding)
                return typeof(PlayerMoveLandState);

            if (isTurning)
                return typeof(PlayerMoveTurnState);

            if (hasInput)
                return typeof(PlayerMoveRunState);

            if (isMoving)
                return typeof(PlayerMoveBreakState);

            return typeof(PlayerMoveIdleState);
        }
    }
}
