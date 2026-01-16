using UnityEngine;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class GetLadderDownState : AbstractState
    {
        protected override void EnterState()
        {
            base.EnterState();

            Body.StopSpeeds();
            Body.Vertical.UseGravity = false;

            Motor.CanChangeInput = false;
            Motor.CanChangeHorizontalDirection = false;
        }

        protected override void ExitState()
        {
            base.ExitState();

            Body.Vertical.UseGravity = true;
            Motor.CanChangeInput = true;
            Motor.CanChangeHorizontalDirection = true;
        }
    }
}