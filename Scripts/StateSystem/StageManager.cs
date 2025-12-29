using System;
using UnityEngine;

namespace MMX.StageSystem
{
    [DisallowMultipleComponent]
    public sealed class StageManager : MonoBehaviour
    {
        public static event Action OnStarted;
        public static event Action OnReadied;
        public static event Action OnReseted;
        public static event Action OnFinished;

        public static void StartState() => OnStarted?.Invoke();
        public static void ReadyStage() => OnReadied?.Invoke();
        public static void ResetStage() => OnReseted?.Invoke();
        public static void FinishState() => OnFinished?.Invoke();
    }
}