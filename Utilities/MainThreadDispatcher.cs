// In Assets/BigTime-SDK/Utilities/MainThreadDispatcher.cs
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BigTime.SDK.Utilities
{
    public class MainThreadDispatcher : MonoBehaviour
    {
        private static readonly Queue<Action> _executionQueue = new Queue<Action>();
        private static MainThreadDispatcher _instance = null;

        public static void Initialize()
        {
            if (_instance == null)
            {
                _instance = new GameObject("MainThreadDispatcher").AddComponent<MainThreadDispatcher>();
                DontDestroyOnLoad(_instance.gameObject);
            }
        }

        public void Update()
        {
            lock (_executionQueue)
            {
                while (_executionQueue.Count > 0)
                {
                    _executionQueue.Dequeue().Invoke();
                }
            }
        }

        public static void Enqueue(Action action)
        {
            lock (_executionQueue)
            {
                _executionQueue.Enqueue(action);
            }
        }
    }
}