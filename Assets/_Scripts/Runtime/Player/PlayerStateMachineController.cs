using System;

namespace ProtoPlat.Player
{
    public abstract class PlayerStateMachineController
    {
        public bool TryTransition(PlayerFrameData frameData, PlayerState currentState, out Type nextStateType)
        {
            nextStateType = GetNextStateType(frameData);
            return nextStateType != null
                && (currentState == null
                    || nextStateType != currentState.GetType());
        }

        protected abstract Type GetNextStateType(PlayerFrameData frameData);
    }
}
