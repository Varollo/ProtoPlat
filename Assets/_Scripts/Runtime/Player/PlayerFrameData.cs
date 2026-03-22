using UnityEngine;

namespace ProtoPlat.Player
{
    public struct PlayerFrameData
    {
        public PlayerDataSO PlayerData;
        public float MoveInput;
        public bool JumpInput;
        public Vector2 Velocity;
        public bool IsGrounded;
    }
}
