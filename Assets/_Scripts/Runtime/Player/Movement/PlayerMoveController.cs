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
