using UnityEngine;
using System.Collections;

namespace MMX.PlayerSystem
{
    public abstract class AbstractHurtState : AbstractState
    {
        [Space]
        [SerializeField] private Vector2 falbackSpeed;

        protected override void EnterState()
        {
            base.EnterState();

            Body.StopSpeeds();
            Body.Vertical.UseGravity = false;

            Player.Jump.enabled = false;
            Player.Dash.enabled = false;

            Motor.IsHurting = true;
            Motor.CanChangeInput = false;
            Motor.CanChangeHorizontalDirection = false;

            var canFallback = !StateMachine.WasExecuting<StuckState>();
            if (!canFallback) return;

            var hasFallbackSpeed = falbackSpeed.sqrMagnitude > 0f;
            if (hasFallbackSpeed)
            {
                Body.Vertical.Speed = falbackSpeed.y;
                Body.Vertical.UseGravity = Body.Vertical.HasSpeed();
                Body.Horizontal.Speed = GetFallbackHorizontalSpeed(falbackSpeed.x);
                return;
            }

            StartCoroutine(Fallback());
        }

        protected override void ExitState()
        {
            base.ExitState();

            Body.StopSpeeds();
            Body.Vertical.UseGravity = true;

            Player.Jump.enabled = true;
            Player.Dash.enabled = true;

            Motor.IsHurting = false;
            Motor.CanChangeInput = true;
            Motor.CanChangeHorizontalDirection = true;

            Player.DamageFeedback.IsPlayingDamageAnimation = false;
        }

        public void Trigger()
        {
            Player.DamageFeedback.IsPlayingDamageAnimation = true;
            ApplyTrigger();
        }

        protected float GetFallbackHorizontalSpeed(float speed) => speed * GetInverseDirection();

        protected abstract void ApplyTrigger();
        protected abstract IEnumerator Fallback();

        private float GetInverseDirection() => -Motor.HorizontalDirection;
    }
}