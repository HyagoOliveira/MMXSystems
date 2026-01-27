using System;
using UnityEngine;

namespace MMX.StageSystem
{
    [DisallowMultipleComponent]
    public sealed class StageManager : MonoBehaviour
    {
        public static event Action OnReadied;
        public static event Action OnStarted;
        public static event Action OnRestarted;
        public static event Action OnFinished;

        public static void Ready() => OnReadied?.Invoke();
        public static void Start_() => OnStarted?.Invoke();
        public static void Restart() => OnRestarted?.Invoke();
        public static void Finish() => OnFinished?.Invoke();
    }
}