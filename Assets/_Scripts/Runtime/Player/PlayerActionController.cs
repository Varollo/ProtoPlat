using System;

namespace ProtoPlat.Player
{
    public class PlayerActionController
    {
        public event Action OnMove;
        public event Action OnStop;
        public event Action OnJump;
        public event Action OnDrop;

        private bool _moving = false;

        public void UpdatePlayerActions(PlayerFrameData frameData)
        {
            if (frameData.MoveInput != 0 && !_moving)
            {
                _moving = true;
                OnMove?.Invoke();
            }
            
            if (frameData.MoveInput == 0 && _moving)
            {
                _moving = false;
                OnStop?.Invoke();
            }

            if (frameData.StartJump && frameData.IsGrounded)
            {
                OnJump?.Invoke();
            }

            if (frameData.StartDrop && frameData.IsOnPlatform)
            {
                OnDrop?.Invoke();
            }
        }
    }
}
