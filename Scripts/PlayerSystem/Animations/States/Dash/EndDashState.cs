using UnityEngine;
using ActionCode.Sidescroller;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(DashState))]
    public sealed class EndDashState : AbstractState
    {
        [SerializeField] private DashState dashState;
        [SerializeField, Min(0f)] private float speed = 8f;

        private sbyte direction;
        private bool isAbleToUpdateSpeed;

        protected override void Reset()
        {
            base.Reset();
            dashState = GetComponent<DashState>();
        }

        protected override void EnterState()
        {
            base.EnterState();

            direction = Motor.HorizontalDirection;
            isAbleToUpdateSpeed = dashState.Input.HasInput && Body.IsGrounded;

            UpdateHorizontalSpeed();
            CheckAirborneDashing();
        }

        protected override void UpdateState()
        {
            base.UpdateState();
            UpdateHorizontalSpeed();
        }

        protected override void ExitState()
        {
            base.ExitState();
            Motor.StopHorizontalMovementIfNoInput();
        }

        private void UpdateHorizontalSpeed()
        {
            if (!isAbleToUpdateSpeed) return;
            Body.Horizontal.Speed = direction * speed;
        }

        private void CheckAirborneDashing()
        {
            const uint arialAnimationFrames = 7;

            void DisableDashing() => dashState.SetIsDashing(false);

            if (!Body.IsAirborne) return;

            dashState.SetIsDashing(true);
            this.ExecuteAfterFrames(arialAnimationFrames, DisableDashing);
        }
    }
}