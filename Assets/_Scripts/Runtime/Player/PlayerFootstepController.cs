using ProtoPlat.Components;
using ProtoPlat.Events;
using UnityEngine;

namespace ProtoPlat.Player
{
    public class PlayerFootstepController : FootstepController
    {
        [Space]
        [SerializeField] private GameEventSO playerEvents;

        private bool _moving = false;

        private void OnEnable()
        {
            playerEvents.AddListener(PlayerEvents.Move, OnMoveStart);
            playerEvents.AddListener(PlayerEvents.Stop, OnMoveStop);
        }

        private void OnDisable()
        {
            playerEvents.RemoveListener(PlayerEvents.Move, OnMoveStart);
            playerEvents.RemoveListener(PlayerEvents.Stop, OnMoveStop);
        }

        private void OnMoveStart()
        {
            _moving = true;
        }

        private void OnMoveStop()
        {
            _moving = false;
        }

        protected override bool CanPlayFootstep()
        {
            return _moving;
        }
    }
}
