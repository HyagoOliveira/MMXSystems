using UnityEngine;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class RayInState : AbstractRayState
    {
        protected override void EnterState()
        {
            base.EnterState();
            Player.Enable();
            Body.EnableCollisions();
        }

        protected override void ExitState()
        {
            base.ExitState();
            Player.EnableInteractions();
        }

        public void Spawn() => Player.Animator.Spawn();
    }
}