using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProtoPlat.Animation
{
    public class SpriteAnimator : MonoBehaviour, IAnimationListener
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private bool autoPlayFirst = true;
        [SerializeField] private AnimNamePair[] animations;

        private Dictionary<string, SpriteAnimationSO> _animMap;
        private int _internalFrameCount;

        public string CurrentAnimationName { get; private set; }

        private void Start()
        {
            if (autoPlayFirst && animations != null && animations.Length > 0)
                Play(animations[0].Name);
        }

        private void OnEnable()
        {
            AnimationManager.AddListener(this);
        }

        private void OnDisable()
        {
            AnimationManager.RemoveListener(this);
        }

        public SpriteAnimationSO GetAnimation(string animationName)
        {
            _animMap ??= animations.ToDictionary(pair => pair.Name, pair => pair.Animation);
            return _animMap[animationName];
        }

        public void Play(string animationName)
        {
            if (string.IsNullOrEmpty(animationName))
                return;

            if (CurrentAnimationName != animationName)
                _internalFrameCount = 0;
            
            CurrentAnimationName = animationName;
        }

        public void UpdateFrame(int frameCount)
        {
            SpriteAnimationSO animation = GetAnimation(CurrentAnimationName);
            Sprite frame = animation.Frames[_internalFrameCount % animation.Frames.Length];

            spriteRenderer.sprite = frame;

            _internalFrameCount = Mathf.Max(0, _internalFrameCount + 1);
        }

#if UNITY_EDITOR
        private void Reset() => spriteRenderer = GetComponent<SpriteRenderer>();
#endif

        [System.Serializable]
        private struct AnimNamePair
        {
            public string Name;
            public SpriteAnimationSO Animation;
        }
    }
}
