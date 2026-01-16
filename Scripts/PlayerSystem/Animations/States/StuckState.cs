using UnityEngine;
using System.Collections;
using ActionCode.EnergySystem;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class StuckState : AbstractState
    {
        protected override void EnterState()
        {
            base.EnterState();
            ResetStuckMotionTime();

            Body.StopSpeeds();
            Body.Vertical.UseGravity = false;

            Motor.TryUnclimb();
            Motor.CanChangeInput = true;
            Motor.CanChangeHorizontalDirection = true;

            Player.Fall.ResetCurrentHorizontalSpeed();
            Player.Damageable.IsInvulnerable = true;
            Player.Damageable.OnDamaged += HandleDamaged;
        }

        protected override void ExitState()
        {
            base.ExitState();

            Body.Vertical.UseGravity = true;

            Motor.CanChangeInput = true;
            Motor.CanChangeHorizontalDirection = true;

            Player.Damageable.IsInvulnerable = false;
            Player.Damageable.OnDamaged -= HandleDamaged;
        }

        public void EnableFor(uint frames)
        {
            Enable();
            this.ExecuteAfterFrames(frames, Disable);
        }

        public void Enable() => SetIsStucked(true);
        public void Disable() => SetIsStucked(false);

        public void DisableAndCancel()
        {
            Disable();
            StopAllCoroutines();
        }

        private void HandleDamaged(Damager damager) => StartCoroutine(PlayHurtAnimation());

        private IEnumerator PlayHurtAnimation()
        {
            var time = 0f;
            ResetStuckMotionTime();

            while (time < 1F)
            {
                yield return null;

                time += Time.deltaTime;
                SetStuckMotionTime(time);
            }

            yield return null;
            ResetStuckMotionTime();
        }

        private void ResetStuckMotionTime() => SetStuckMotionTime(0F);
        private void SetIsStucked(bool enabled) => Player.Animator.IsStucked = enabled;
        private void SetStuckMotionTime(float value) => Player.Animator.StuckMotionTime = value;
    }
}