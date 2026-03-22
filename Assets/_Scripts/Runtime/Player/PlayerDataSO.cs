using UnityEngine;

namespace ProtoPlat
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Ribbons/Player Data")]
    public class PlayerDataSO : ScriptableObject
    {
        #region Movement
        [Header("Movement")]
        [SerializeField] private float maxSpeed = 10f;
        [SerializeField] private float accRate = 1f;
        [SerializeField, Range(0f, 1f)] private float breakFriction = 0.9f;
        [SerializeField, Range(0f, 1f)] private float turnFriction = 0.75f;

        public float MaxSpeed => maxSpeed;
        public float AccRate => accRate;
        public float BreakFriction => breakFriction;
        public float TurnFriction => turnFriction;
        #endregion

        #region Jump
        [Header("Jump")]
        [SerializeField] private float jumpForce = 500f;
        [SerializeField, Min(1f)] private float fallSpeedMult = 1.1f;
        [SerializeField, Min(1f)] private float glideSpeedMult = 1.05f;
        [SerializeField, Range(0f, 1f)] private float riseSpeedMult = .9f;
        [SerializeField, Range(0f, 1f)] private float floatSpeedMult = 1f;

        public float JumpForce => jumpForce;
        public float FallSpeedMult => fallSpeedMult;
        public float GlideSpeedMult => glideSpeedMult;
        public float RiseSpeedMult => riseSpeedMult;
        public float FloatSpeedMult => floatSpeedMult;
        #endregion
    }
}
