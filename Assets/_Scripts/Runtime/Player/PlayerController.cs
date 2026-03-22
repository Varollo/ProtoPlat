using ProtoPlat.Animation;
using ProtoPlat.Input;
using ProtoPlat.Player.Jump;
using ProtoPlat.Player.Jump.States;
using ProtoPlat.Player.Movement;
using ProtoPlat.Player.Movement.States;
using TMPro;
using UnityEngine;

namespace ProtoPlat.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private readonly PlayerMoveController _moveController = new();
        private readonly PlayerMoveStateMachine _moveStateMachine = new(
            new PlayerMoveIdleState(),
            new PlayerMoveRunState(),
            new PlayerMoveBreakState(),
            new PlayerMoveTurnState(),
            new PlayerMoveLandState()
        );

        private readonly PlayerJumpController _jumpController = new();
        private readonly PlayerJumpStateMachine _jumpStateMachine = new(
            new PlayerJumpFallState(),
            new PlayerJumpRiseState(),
            new PlayerJumpGlideState(),
            new PlayerJumpFloatState()
        );

        [SerializeField] private PlayerDataSO playerData;
        [SerializeField] private SpriteAnimator animator;
        [SerializeField] private GroundChecker groundChecker;
        [Header("Debug")]
        [SerializeField] private TMP_Text debugText;

        private Rigidbody2D _rb;
        private PlayerFrameData _currentFrameData;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
#if !UNITY_EDITOR
            debugText.gameObject.SetActive(false);
#endif
            UpdateFrameData();
            InitialzeStateMachines();
        }

        private void Update()
        {
            UpdateFrameData();
            UpdateControllers();
            UpdateAnimation();
        }

        private void FixedUpdate()
        {
            _rb.linearVelocityX = Mathf.Clamp(_moveStateMachine.GetVelocityX(_currentFrameData),
                -playerData.MaxVelocity.x, playerData.MaxVelocity.x);
            
            _rb.linearVelocityY = Mathf.Clamp(_jumpStateMachine.GetVelocityY(_currentFrameData), 
                -playerData.MaxVelocity.y, playerData.MaxVelocity.y);
        }

        private void InitialzeStateMachines()
        {
            _moveStateMachine.Transition<PlayerMoveIdleState>();
            _jumpStateMachine.Transition<PlayerJumpFallState>();
        }

        private void UpdateFrameData()
        {
            _currentFrameData = new()
            {
                PlayerData = playerData,
                MoveInput = InputManager.Move.Value.x,
                JumpInput = InputManager.Jump.IsPressed,
                Velocity = _rb.linearVelocity,
                IsGrounded = groundChecker.IsGrounded,
            };
        }

        private void UpdateControllers()
        {
            if (_jumpController.TryJump(_currentFrameData, out var jumpForce))
            {
                _rb.linearVelocityY = 0;
                _rb.AddForceY(jumpForce, ForceMode2D.Force);
            }

            if (_moveController.TryTransition(_currentFrameData, _moveStateMachine.CurrentState, out var nextMoveState))
                _moveStateMachine.Transition(nextMoveState);

            if (_jumpController.TryTransition(_currentFrameData, _jumpStateMachine.CurrentState, out var nextJumpState))
                _jumpStateMachine.Transition(nextJumpState);
        }

        private void UpdateAnimation()
        {
            string anim = _currentFrameData.IsGrounded 
                ? _moveStateMachine.CurrentAnimationName 
                : _jumpStateMachine.CurrentAnimationName;

            animator.Play(anim);

#if UNITY_EDITOR
            debugText.text = anim;
#endif
        }
    }
}
