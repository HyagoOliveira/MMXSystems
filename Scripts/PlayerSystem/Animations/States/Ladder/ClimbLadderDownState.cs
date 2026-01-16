using UnityEngine;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class ClimbLadderDownState : AbstractState
    {
        private const float verticalDisplacement = -2.3125F;

        protected override void EnterState()
        {
            base.EnterState();
            Player.ClimbLadder.Climb();

            Motor.CanChangeInput = false;
            Motor.CanChangeHorizontalDirection = false;
        }

        protected override void ExitState()
        {
            base.ExitState();
            Body.AddPositionY(verticalDisplacement);

            Motor.CanChangeInput = true;
            Motor.CanChangeHorizontalDirection = true;
        }
    }
}