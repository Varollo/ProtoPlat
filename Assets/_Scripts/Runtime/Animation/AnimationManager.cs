using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace ProtoPlat.Animation
{
    public static class AnimationManager
    {
        public const float FrameDuration = 0.1f;

        private readonly static LinkedList<IAnimationListener> _listeners = new();

        private static float _timeCounter;
        private static int _frameCount;

        public static void AddListener(IAnimationListener listener) => _listeners.AddLast(listener);
        public static void RemoveListener(IAnimationListener listener) => _listeners.Remove(listener);

        private static void AnimationUpdate()
        {
            _timeCounter += Time.deltaTime;

            if (_timeCounter >= FrameDuration)
            {
                _timeCounter = 0;
                _frameCount = Mathf.Max(_frameCount + 1, 0);

                foreach(var listener in _listeners)
                    if (listener != null || listener is Component comp && comp)
                        listener.UpdateFrame(_frameCount);
            }
        }

        [RuntimeInitializeOnLoadMethod]
        private static void Init()
        {
            var currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
            var systems = new List<PlayerLoopSystem>(currentPlayerLoop.subSystemList);

            PlayerLoopSystem customSystem = new()
            {
                type = typeof(SpriteAnimationUpdate),
                updateDelegate = AnimationUpdate
            };

            for (int i = 0; i < systems.Count; i++)
            {
                var system = systems[i];

                if (system.type == typeof(PreLateUpdate))
                {
                    var subSystems = new List<PlayerLoopSystem>(system.subSystemList)
                    {
                        customSystem
                    };

                    system.subSystemList = subSystems.ToArray();
                    systems[i] = system;

                    break;
                }
            }

            currentPlayerLoop.subSystemList = systems.ToArray();
            PlayerLoop.SetPlayerLoop(currentPlayerLoop);
        }
    }

    public struct SpriteAnimationUpdate { }
}
