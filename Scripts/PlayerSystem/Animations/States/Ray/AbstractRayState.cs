using UnityEngine;

namespace MMX.PlayerSystem
{
    public abstract class AbstractRayState : AbstractState
    {
        [SerializeField] private float speed = 16f;

        protected override void EnterState()
        {
            base.EnterState();

            Player.Animator.IsRay = true;
            Player.DisableInteractions();

            Body.enabled = true;
            Body.Vertical.Speed = speed;
        }

        protected override void ExitState()
        {
            base.ExitState();

            Player.Animator.IsRay = false;
            Body.Vertical.UseGravity = true;
        }
    }
}