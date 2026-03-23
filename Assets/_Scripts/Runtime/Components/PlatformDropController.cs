using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtoPlat.Components
{
    public class PlatformDropController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D targetRigidbody;
        [SerializeField, Min(0)] private float fallThroughDurationSec = 0.3f;
        
        private bool _collidable = true;

        // Failsafe for stopping the coroutine midway
        private void OnDisable()
        {
            _collidable = true;
        }

        public void Drop()
        {
            if (_collidable)
                StartCoroutine(DropdownSequence());
        }

        private IEnumerator DropdownSequence()
        {
            var colliders = new List<Collider2D>();
            targetRigidbody.GetAttachedColliders(colliders);

            foreach (var collider in colliders)
                collider.enabled = false;
            _collidable = false;

            yield return new WaitForSeconds(fallThroughDurationSec);

            foreach (var collider in colliders)
                collider.enabled = true;
            _collidable = true;
        }
    }
}
