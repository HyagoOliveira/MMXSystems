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
        public event Action OnFootstep;

        private void Footstep() => OnFootstep?.Invoke();
    }
}
