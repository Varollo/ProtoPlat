using ProtoPlat.Animation;
using ProtoPlat.Events;
using System;
using UnityEngine;

namespace ProtoPlat.Player
{
    public class PlayerSoundController : MonoBehaviour
    {
        [SerializeField] private SpriteAnimator animator;
        [SerializeField] private GameEventSO playerEvents;
        [Space]
        [SerializeField] private AudioSource breakAS;
        [SerializeField] private AudioSource landAS;
        [SerializeField] private AudioSource jumpAS;

        private void OnEnable()
        {
            animator.OnAnimationChange.AddListener(OnAnimationChange);
            playerEvents.AddListener(PlayerEvents.Jump, OnJump);
        }

        private void OnDisable()
        {
            animator.OnAnimationChange.RemoveListener(OnAnimationChange);
            playerEvents.RemoveListener(PlayerEvents.Jump, OnJump);
        }

        private void OnAnimationChange(string animName)
        {
            switch (animName)
            {
                case "break":
                    breakAS.Play(); break;

                case "land":
                    landAS.Play(); break;
            }
        }

        private void OnJump()
        {
            jumpAS.Play();
        }
    }
}
