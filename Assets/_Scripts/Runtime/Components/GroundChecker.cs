using UnityEngine;

namespace ProtoPlat
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private float detectionRadius = 1f;

        private bool? _grounded;

        public bool IsGrounded
        {
            get
            {
                if (!_grounded.HasValue)
                    _grounded = Physics2D.OverlapCircle(transform.position, detectionRadius, groundLayerMask);
                return _grounded.Value;
            }
        }

        private void LateUpdate()
        {
            if (_grounded.HasValue)
                _grounded = null;
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
