using UnityEngine;

namespace ProtoPlat.Components
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private LayerMask platformLayerMask;
        [SerializeField] private float detectionRadius = 1f;

        private bool? _grounded;
        private bool? _onPlatform;

        public bool IsGrounded
        {
            get
            {
                if (!_grounded.HasValue)
                    _grounded = Physics2D.OverlapCircle(transform.position, detectionRadius, groundLayerMask);
                return _grounded.Value;
            }
        }

        public bool IsOnPlatform
        {
            get
            {
                if (!_onPlatform.HasValue)
                    _onPlatform = Physics2D.OverlapCircle(transform.position, detectionRadius, platformLayerMask);
                return _onPlatform.Value;
            }
        }

        private void LateUpdate()
        {
            if (_grounded.HasValue)
                _grounded = null;

            if (_onPlatform.HasValue)
                _onPlatform = null;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
#endif
    }
}
