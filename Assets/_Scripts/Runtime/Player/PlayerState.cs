using ProtoPlat.StateMachines;
using UnityEngine;

namespace ProtoPlat.Player
{
    public abstract class PlayerState : IAnimatedState
    {
        private float _enterTime;
        private float _exitTime;

        public abstract string AnimationName { get; }
        
        protected float ElapsedTime => Time.time - _enterTime;
        protected float CooldownTime => Time.time - _exitTime;

        public virtual bool CanEnter() => true;
        public virtual bool CanExit() => true;

        public virtual void EnterState() => _enterTime = Time.time;
        public virtual void ExitState() => _exitTime = Time.time;
    }
}
