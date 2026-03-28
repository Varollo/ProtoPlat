using System;
using UnityEngine;

namespace ProtoPlat.Components
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField, Min(float.Epsilon)] private float smoothing = 60f;
        [SerializeField] private Vector2 offset;
        [SerializeField, Range(0f, 1f)] private float lookAhead = 0f;
        [SerializeField] private Vector2 deadZone = Vector2.one;
        [SerializeField] private FollowUpdateType updateType = FollowUpdateType.Normal;

        private Vector3? _lastFocalPoint;
        private Vector2 _lookAheadOffset;
        private Vector3 _focalPoint;

        public Vector3 FocalPoint => _focalPoint;

        private void Update()
        {
            Follow(FollowUpdateType.Normal);
        }

        private void FixedUpdate()
        {
            Follow(FollowUpdateType.Fixed);
        }

        private void Follow(FollowUpdateType targetUpdate)
        {
            if (updateType != targetUpdate)
                return;

            float deltaTime = (targetUpdate) switch
            {
                FollowUpdateType.Normal => Time.deltaTime,
                FollowUpdateType.Fixed => Time.fixedDeltaTime,
                _ => 0f
            };

            // Better frame-rate based interpolation
            float interpolation = 1f - Mathf.Exp(-smoothing * deltaTime);

            // Initialize focal point on first frame
            if (!_lastFocalPoint.HasValue)
                _focalPoint = target.position;

            // Calculate focal point based on deadzone
            Vector3 diff = target.position - _focalPoint;
            if (Mathf.Abs(diff.x) > deadZone.x)
                _focalPoint.x = target.position.x - (Mathf.Sign(diff.x) * deadZone.x);
            if (Mathf.Abs(diff.y) > deadZone.y)
                _focalPoint.y = target.position.y - (Mathf.Sign(diff.y) * deadZone.y);

            // Look-ahead is target's velocity by delta time
            // If delta time is 0 it skips the calculation this frame
            if (deltaTime > 0)
                _lookAheadOffset = Vector2.Lerp(_lookAheadOffset, 
                    (_lastFocalPoint.HasValue
                        ? _focalPoint - _lastFocalPoint.Value
                        : Vector3.zero) / deltaTime * lookAhead,
                    interpolation);

            Vector3 targetPos = _focalPoint + (Vector3)(offset + _lookAheadOffset);

            Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos, interpolation);
            smoothedPos.z = transform.position.z;

            transform.position = smoothedPos;

            _lastFocalPoint = _focalPoint;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position - (Vector3)offset, deadZone * 2);
        }
#endif

        [Serializable]
        public enum FollowUpdateType
        {
            Normal,
            Fixed
        }
    }
}
