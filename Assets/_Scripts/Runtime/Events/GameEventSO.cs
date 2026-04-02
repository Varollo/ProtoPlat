using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace ProtoPlat.Events
{
    [CreateAssetMenu(fileName = "New Game Event", menuName = "Ribbons/Events/Game Event")]
    public class GameEventSO : ScriptableObject
    {
        private Dictionary<string, UnityEvent> _events;

        [SerializeField] private GameEventData[] eventData;

        private Dictionary<string, UnityEvent> Events => 
            _events ??= eventData.ToDictionary(data => data.Name, data => data.Event);

        public void Raise(string eventName)
        {
            if (TryGetEvent(eventName, out var e))
                e?.Invoke();
        }

        public void AddListener(string eventName, UnityAction call)
        {
            if (TryGetEvent(eventName, out var e))
                e.AddListener(call);
        }

        public void RemoveListener(string eventName, UnityAction call)
        {
            if (TryGetEvent(eventName, out var e))
                e.RemoveListener(call);
        }

        private bool TryGetEvent(string eventName, out UnityEvent e)
        {
            if (Events.TryGetValue(eventName, out e))
                return true;

            Debug.LogWarning($"Couldn't find game event with name '{eventName}'");
            return false;
        }

#if UNITY_EDITOR
        private void OnValidate() => _events = null;
#endif

        [System.Serializable]
        private struct GameEventData
        {
            public string Name;
            public UnityEvent Event;
        }
    }

    public class GameEventSO<TArg> : ScriptableObject
    {
        private Dictionary<string, ArgUnityEvent> _events;

        [SerializeField] private GameEventData[] eventData;

        private Dictionary<string, ArgUnityEvent> Events =>
            _events ??= eventData.ToDictionary(data => data.Name, data => data.Event);

        public void Raise(string eventName, TArg arg)
        {
            if (TryGetEvent(eventName, out var e))
                e?.Invoke(arg);
        }

        public void AddListener(string eventName, UnityAction<TArg> call)
        {
            if (TryGetEvent(eventName, out var e))
                e.AddListener(call);
        }

        public void RemoveListener(string eventName, UnityAction<TArg> call)
        {
            if (TryGetEvent(eventName, out var e))
                e.RemoveListener(call);
        }

        private bool TryGetEvent(string eventName, out ArgUnityEvent e)
        {
            if (Events.TryGetValue(eventName, out e))
                return true;

            Debug.LogWarning($"Couldn't find game event with name '{eventName}'");
            return false;
        }

#if UNITY_EDITOR
        private void OnValidate() => _events = null;
#endif

        [System.Serializable]
        private struct GameEventData
        {
            public string Name;
            public ArgUnityEvent Event;
        }

        private class ArgUnityEvent : UnityEvent<TArg> { }
    }
}
