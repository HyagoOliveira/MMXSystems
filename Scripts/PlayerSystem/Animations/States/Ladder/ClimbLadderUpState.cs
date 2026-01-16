using UnityEngine;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class ClimbLadderUpState : AbstractState
    {
        private const float verticalSpeed = 3.75F;

        protected override void EnterState()
        {
            base.EnterState();

            Player.ClimbLadder.Climb();
            Body.Vertical.Speed = verticalSpeed;

            Motor.CanChangeInput = false;
            Motor.CanChangeHorizontalDirection = false;
        }

        protected override void ExitState()
        {
            base.ExitState();
            Body.Vertical.StopSpeed();
            Motor.CanChangeInput = true;
            Motor.CanChangeHorizontalDirection = true;
        }
    }
}