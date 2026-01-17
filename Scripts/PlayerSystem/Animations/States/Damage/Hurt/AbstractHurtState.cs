using System.Collections;

namespace MMX.PlayerSystem
{
    public abstract class AbstractHurtState : AbstractState
    {
        protected override void EnterState()
        {
            base.EnterState();

            Body.StopSpeeds();
            Body.Vertical.UseGravity = false;

            Motor.IsHurting = true;
            Motor.CanChangeInput = false;
            Motor.CanChangeHorizontalDirection = false;

            var shouldFallback = !StateMachine.WasExecuting<StuckState>();
            if (shouldFallback) StartCoroutine(Fallback());
        }

        protected override void ExitState()
        {
            base.ExitState();
            Body.Vertical.UseGravity = true;

            Motor.IsHurting = false;
            Motor.CanChangeInput = true;
            Motor.CanChangeHorizontalDirection = true;

            Player.Damageable.feedback.IsPlayingDamageAnimation = false;
        }

        public void Trigger()
        {
            Player.Damageable.feedback.IsPlayingDamageAnimation = true;
            ApplyTrigger();
        }

        protected float GetFallbackHorizontalSpeed(float speed) => speed * GetInverseDirection();

        protected abstract void ApplyTrigger();
        protected abstract IEnumerator Fallback();

        private float GetInverseDirection() => -Motor.HorizontalDirection;
    }
}