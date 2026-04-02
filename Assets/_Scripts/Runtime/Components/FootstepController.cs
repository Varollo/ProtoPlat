using UnityEngine;

namespace ProtoPlat.Components
{
    public abstract class FootstepController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GroundChecker groundChecker;
        [Header("Config")]
        [SerializeField, Range(-3f, 3f)] private float minPichShift = 1f;
        [SerializeField, Range(-3f, 3f)] private float maxPichShift = 1f;

        private void Update()
        {
            if (audioSource.isPlaying)
                return;

            if (!CanPlayFootstep())
                return;

            var ground = groundChecker.GetGround();

            if (!ground)
                return;

            // check what kind of ground it is and play a different sound

            audioSource.pitch = Random.Range(minPichShift, maxPichShift);
            audioSource.Play();
        }

        protected abstract bool CanPlayFootstep();

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (minPichShift > maxPichShift)
                minPichShift = maxPichShift;
        }
#endif
    }
}
