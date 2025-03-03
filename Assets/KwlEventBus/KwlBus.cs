using System;
using System.Collections.Generic;
using UnityEngine;

namespace KwlEventBus
{
    public static class KwlBus<TEvent> where TEvent : struct
    {
        private static readonly List<Action<TEvent>> Listeners = new List<Action<TEvent>>();

        public static void AddListener(Action<TEvent> listener)
        {
            if (!Listeners.Contains(listener))
            {
                Listeners.Add(listener);
                Debug.Log($"KwlBus: Listener added for {typeof(TEvent).Name}");
            }
        }

        public static void RemoveListener(Action<TEvent> listener)
        {
            if (Listeners.Contains(listener))
            {
                Listeners.Remove(listener);
                Debug.Log($"KwlBus: Listener removed for {typeof(TEvent).Name}");
            }
        }

        public static void NotifyListeners(TEvent eventData)
        {
            Debug.Log($"KwlBus: Notifying event {typeof(TEvent).Name} to {Listeners.Count} listeners.");

            for (int i = Listeners.Count - 1; i >= 0; i--)
            {
                try
                {
                    Listeners[i]?.Invoke(eventData);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"EventBus: Error while invoking listener: {ex.Message}");
                }
            }
        }

        public static int GetListenerCount()
        {
            return Listeners.Count;
        }
    }
}
