using System;
using UnityEngine;

namespace MMX.CharacterSystem
{
    /// <summary>
    /// Receive events from Animations and invoke the respective Actions.
    /// </summary>
    /// <remarks>
    /// It must be in the same GameObject where an Animator is.
    /// </remarks>
    [DisallowMultipleComponent]
    public sealed class AnimationEvents : MonoBehaviour
    {
        public event Action OnVictory;
        public event Action OnGetReadied;
        public event Action OnFootstep;
        public event Action OnDestroyed;

        private void Victory() => OnVictory?.Invoke();
        private void GetReady() => OnGetReadied?.Invoke();
        private void Footstep() => OnFootstep?.Invoke();
        private void Destroy() => OnFootstep?.Invoke();
    }
}