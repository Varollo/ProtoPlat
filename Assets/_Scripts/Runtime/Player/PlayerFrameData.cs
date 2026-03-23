using UnityEngine;

namespace ProtoPlat.Player
{
    public struct PlayerFrameData
    {
        public PlayerDataSO PlayerData;
        public float MoveInput;
        public bool JumpInput;
        public bool DropInput;
        public bool StartJump;
        public bool StartDrop;
        public Vector2 Velocity;
        public bool IsGrounded;
        public bool IsOnPlatform;
    }
}
