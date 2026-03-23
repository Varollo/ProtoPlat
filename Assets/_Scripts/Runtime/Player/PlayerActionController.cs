using System;

namespace ProtoPlat.Player
{
    public class PlayerActionController
    {
        public event Action OnJump;
        public event Action OnDrop;

        public void UpdatePlayerActions(PlayerFrameData frameData)
        {
            if (frameData.StartJump && frameData.IsGrounded)
                OnJump?.Invoke();

            if (frameData.StartDrop && frameData.IsOnPlatform)
                OnDrop?.Invoke();
        }
    }
}
