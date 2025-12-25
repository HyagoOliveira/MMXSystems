using UnityEngine;
using ActionCode.Sidescroller;

namespace MMX.PlayerSystem
{
    [RequireComponent(typeof(DashState))]
    public sealed class StartDashState : AbstractState
    {
        [SerializeField] private DashState dashState;

        private bool wasFromWallSlide;

        protected override void Reset()
        {
            base.Reset();
            dashState = GetComponent<DashState>();
        }

        protected override void EnterState()
        {
            base.EnterState();

            if (Body.IsAirborne)
            {
                Body.Vertical.UseGravity = false;
                wasFromWallSlide = WasExecutingAnyWallState();
                if (wasFromWallSlide) Motor.InvertHorizontalDirection();
            }

            Body.Vertical.StopSpeed();
            Motor.CanChangeHorizontalDirection = false;

            dashState.CheckInvalidDash();
        }

        protected override void UpdateState()
        {
            base.UpdateState();
            dashState.CheckInvalidDash();
        }

        protected override void ExitState()
        {
            base.ExitState();

            Body.Vertical.UseGravity = true;
            Motor.CanChangeHorizontalDirection = !wasFromWallSlide;

            wasFromWallSlide = false;
        }

        private bool WasExecutingAnyWallState() =>
            StateMachine.WasExecuting<WallGrabState>() ||
            StateMachine.WasExecuting<WallSlideState>();
    }
}