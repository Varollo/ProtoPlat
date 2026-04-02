using ProtoPlat.Animation;
using ProtoPlat.Components;
using ProtoPlat.Events;
using ProtoPlat.Input;
using ProtoPlat.Player.Jump;
using ProtoPlat.Player.Movement;
using ProtoPlat.Player.Movement.States;
using System;
using System.Collections;
using UnityEngine;

namespace ProtoPlat.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private readonly PlayerActionController _actionController = new();

        private readonly PlayerMoveController _moveController = new();
        private readonly PlayerJumpController _jumpController = new();

        private readonly PlayerMoveStateMachine _moveStateMachine = new();
        private readonly PlayerJumpStateMachine _jumpStateMachine = new();

        [SerializeField] private PlayerDataSO playerData;
        [SerializeField] private SpriteAnimator animator;
        [SerializeField] private GroundChecker groundChecker;
        [SerializeField] private PlatformDropController platformDropper;
        [SerializeField] private GameEventSO playerEvents;

        private Rigidbody2D _rb;
        private PlayerFrameData _currentFrameData;

        private bool _controllable = true;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _moveStateMachine.OnStateChange += OnMoveStateChange;
            _jumpStateMachine.OnStateChange += OnJumpStateChange;

            _actionController.OnJump += PlayerJumpHandler;
            _actionController.OnDrop += PlayerDropHandler;
        }

        private void OnDisable()
        {
            _actionController.OnJump -= PlayerJumpHandler;
            _actionController.OnDrop -= PlayerDropHandler;
        }

        private void Start()
        {
            UpdateFrameData();
        }

        private void Update()
        {
            if (!_controllable)
                return;

            UpdateFrameData();
            UpdatePlayerActions();
            UpdateControllers();
            UpdateAnimation();
        }

        private void FixedUpdate()
        {
            if (!_controllable)
                return;

            UpdateVelocity();
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!_controllable)
                return;

            if (collision.collider.CompareTag("Harmful"))
                StartCoroutine(HurtSequence(collision.GetContact(0).normal));
        }

        private IEnumerator HurtSequence(Vector2 collisionNormal)
        {
            _controllable = false;

            animator.Play("hurt");
            _rb.AddForce(new(collisionNormal.x * 15 + UnityEngine.Random.Range(-10f, 10f), collisionNormal.y * 20), ForceMode2D.Impulse);
            yield return new WaitForSeconds(.4f);

            _controllable = true;
        }

        private void UpdateFrameData()
        {
            bool isPressingDown = InputManager.Move.Value.y < 0;
            Vector2 lateVelocity = _currentFrameData.Velocity;

            _currentFrameData = new()
            {
                PlayerData = playerData,
                MoveInput = InputManager.Move.Value.x,
                JumpInput = InputManager.Jump.IsPressed,
                DropInput = isPressingDown,
                StartJump = InputManager.Jump.IsJustPressed,
                StartDrop = InputManager.Move.IsJustPressed && isPressingDown,
                Velocity = _rb.linearVelocity,
                LateVelocity = lateVelocity,
                IsGrounded = groundChecker.IsGrounded,
                IsOnPlatform = groundChecker.IsOnPlatform,
            };
        }

        private void UpdatePlayerActions()
        {
            _actionController.UpdatePlayerActions(_currentFrameData);
        }

        private void UpdateControllers()
        {
            if (_moveController.TryTransition(_currentFrameData, _moveStateMachine.CurrentState, out var nextMoveState))
                _moveStateMachine.Transition(nextMoveState);

            if (_jumpController.TryTransition(_currentFrameData, _jumpStateMachine.CurrentState, out var nextJumpState))
                _jumpStateMachine.Transition(nextJumpState);
        }

        private void UpdateAnimation()
        {
            animator.Play(_currentFrameData.IsGrounded
                ? _moveStateMachine.CurrentAnimationName
                : _jumpStateMachine.CurrentAnimationName
            );
        }

        private void UpdateVelocity()
        {
            _rb.linearVelocityX = Mathf.Clamp(_moveStateMachine.GetVelocityX(_currentFrameData),
                -playerData.MaxVelocity.x, playerData.MaxVelocity.x);

            _rb.linearVelocityY = Mathf.Clamp(_jumpStateMachine.GetVelocityY(_currentFrameData),
                -playerData.MaxVelocity.y, playerData.MaxVelocity.y);
        }

        private void PlayerDropHandler()
        {
            platformDropper.Drop();
        }

        private void PlayerJumpHandler()
        {
            _rb.linearVelocityY = 0;
            _rb.AddForceY(playerData.JumpForce);

            playerEvents.Raise(PlayerEvents.Jump);
        }

        private void OnMoveStateChange(Type newStateType)
        {
            if (newStateType == typeof(PlayerMoveRunState))
                playerEvents.Raise(PlayerEvents.Move);

            else if (newStateType == typeof(PlayerMoveBreakState))
                playerEvents.Raise(PlayerEvents.Stop);
        }

        private void OnJumpStateChange(Type newStateType)
        {
        }
    }
}
