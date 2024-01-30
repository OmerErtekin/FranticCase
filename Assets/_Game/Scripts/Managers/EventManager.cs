using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Managers
{
    [DefaultExecutionOrder(-99)]
    public class EventManager : MonoBehaviour
    {
        private readonly Dictionary<Delegate, Delegate> _eventDictionary = new();
        private static EventManager _eventManager;

        #region Logic
        private static EventManager Instance
        {
            get
            {
                if (_eventManager)
                {
                    return _eventManager;
                }
                
                _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!_eventManager)
                {
                    Debug.LogWarning("There needs to be one active EventManager script on a GameObject in your scene.");
                }
 
                return _eventManager;
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private static void AddListener(Delegate eventType, Delegate listener)
        {
            if (Instance._eventDictionary.ContainsKey(eventType))
            {
                Instance._eventDictionary[eventType] = Delegate.Combine(Instance._eventDictionary[eventType], listener);
            }
            else
            {
                Instance._eventDictionary[eventType] = listener;
            }
        }

        private static void RemoveListener(Delegate eventType, Delegate listener)
        {
            if (_eventManager == null || !Instance._eventDictionary.ContainsKey(eventType))
            {
                return;
            }

            Instance._eventDictionary[eventType] = Delegate.Remove(Instance._eventDictionary[eventType], listener);

            if (Instance._eventDictionary[eventType] == null)
            {
                Instance._eventDictionary.Remove(eventType);
            }
        }

        private static void InvokeEvent(Delegate eventType, params object[] parameter)
        {
            if (!Instance._eventDictionary.TryGetValue(eventType, out var thisEvent))
            {
                return;
            }

            var callbacks = thisEvent.GetInvocationList();

            foreach (var callback in callbacks)
            {
                callback?.DynamicInvoke(parameter);
            }
        }

        #endregion

        #region Overloads
        public static void StartListening(Action eventType, Action listener)
        {
            AddListener(eventType, listener);
        }

        public static void StartListening<T>(Action<T> eventType, Action<T> listener)
        {
            AddListener(eventType, listener);
        }

        public static void StartListening<T1, T2>(Action<T1, T2> eventType, Action<T1, T2> listener)
        {
            AddListener(eventType, listener);
        }

        public static void StartListening<T1, T2, T3>(Action<T1, T2, T3> eventType, Action<T1, T2, T3> listener)
        {
            AddListener(eventType, listener);
        }

        public static void StartListening<T1, T2, T3, T4>(Action<T1, T2, T3, T4> eventType,
            Action<T1, T2, T3, T4> listener)
        {
            AddListener(eventType, listener);
        }

        public static void StartListening<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> eventType,
            Action<T1, T2, T3, T4, T5> listener)
        {
            AddListener(eventType, listener);
        }

        public static void StopListening(Action eventType, Action listener)
        {
            RemoveListener(eventType, listener);
        }

        public static void StopListening<T>(Action<T> eventType, Action<T> listener)
        {
            RemoveListener(eventType, listener);
        }

        public static void StopListening<T1, T2>(Action<T1, T2> eventType, Action<T1, T2> listener)
        {
            RemoveListener(eventType, listener);
        }

        public static void StopListening<T1, T2, T3>(Action<T1, T2, T3> eventType, Action<T1, T2, T3> listener)
        {
            RemoveListener(eventType, listener);
        }

        public static void StopListening<T1, T2, T3, T4>(Action<T1, T2, T3, T4> eventType,
            Action<T1, T2, T3, T4> listener)
        {
            RemoveListener(eventType, listener);
        }

        public static void StopListening<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> eventType,
            Action<T1, T2, T3, T4, T5> listener)
        {
            RemoveListener(eventType, listener);
        }

        public static void TriggerEvent(Action eventType)
        {
            InvokeEvent(eventType);
        }

        public static void TriggerEvent<T>(Action<T> eventType, T parameter)
        {
            InvokeEvent(eventType, parameter);
        }

        public static void TriggerEvent<T1, T2>(Action<T1, T2> eventType, T1 parameter1, T2 parameter2)
        {
            InvokeEvent(eventType, parameter1, parameter2);
        }

        public static void TriggerEvent<T1, T2, T3>(Action<T1, T2, T3> eventType, T1 parameter1, T2 parameter2,
            T3 parameter3)
        {
            InvokeEvent(eventType, parameter1, parameter2, parameter3);
        }

        public static void TriggerEvent<T1, T2, T3, T4>(Action<T1, T2, T3, T4> eventType, T1 parameter1, T2 parameter2,
            T3 parameter3, T4 parameter4)
        {
            InvokeEvent(eventType, parameter1, parameter2, parameter3, parameter4);
        }

        public static void TriggerEvent<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> eventType, T1 parameter1,
            T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5)
        {
            InvokeEvent(eventType, parameter1, parameter2, parameter3, parameter4, parameter5);
        }
        #endregion

        #region Actions
        public static readonly Action OnPlayerStartToMove = () => { };
        public static readonly Action OnPlayerStop = () => { };
        #endregion
    }
}