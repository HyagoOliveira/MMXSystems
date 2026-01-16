using UnityEngine;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class GetLadderUpState : AbstractState
    {
        protected override void EnterState()
        {
            base.EnterState();

            Player.ClimbLadder.Unclimb();
            Body.Vertical.FixOnGround();

            Motor.CanChangeInput = false;
            Motor.CanChangeHorizontalDirection = false;
        }

        protected override void ExitState()
        {
            base.ExitState();

            Motor.CanChangeInput = true;
            Motor.CanChangeHorizontalDirection = true;
        }
    }
}