using UnityEngine;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class GetOutState : AbstractState
    {
        protected override void EnterState()
        {
            base.EnterState();

            Player.DisableInteractions();

            Body.DisableCollisions();
            Body.Vertical.UseGravity = false;
        }

        public void GetOut() => Player.Animator.GetOut();
    }
}