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
        [SerializeField] private FollowUpdateType updateType = FollowUpdateType.Normal;

        private Vector3? _targetLastPos;
        private Vector2 _lookAheadOffset;

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

            // Look-ahead is target's velocity by delta time
            // If delta time is 0 it skips the calculation this frame
            if (deltaTime > 0)
                _lookAheadOffset = Vector2.Lerp(_lookAheadOffset, 
                    (_targetLastPos.HasValue
                        ? target.position - _targetLastPos.Value
                        : Vector3.zero) / deltaTime * lookAhead,
                    interpolation);

            Vector3 finalPos = target.position + (Vector3)(offset + _lookAheadOffset);

            Vector3 newPos = Vector3.Lerp(transform.position, finalPos, interpolation);
            newPos.z = transform.position.z;

            transform.position = newPos;
            _targetLastPos = target.position;
        }

        [Serializable]
        public enum FollowUpdateType
        {
            Normal,
            Fixed
        }
    }
}
