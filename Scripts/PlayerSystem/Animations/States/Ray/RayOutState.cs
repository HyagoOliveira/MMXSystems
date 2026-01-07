using UnityEngine;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class RayOutState : AbstractRayState
    {
        [SerializeField, Min(0f)] private float timeToDisable = 1f;

        protected override void EnterState()
        {
            Body.DisableCollisions();
            base.EnterState();
            Invoke(nameof(Disable), timeToDisable);
        }

        private void Disable() => Player.Disable();
    }
}