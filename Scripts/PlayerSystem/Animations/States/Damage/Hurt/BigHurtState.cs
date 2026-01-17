using UnityEngine;
using System.Collections;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class BigHurtState : AbstractHurtState
    {
        protected override void ApplyTrigger() => Player.Animator.BigHurt();

        protected override IEnumerator Fallback()
        {
            const float verticalSpeed = 9.3375F;
            const float groundedHorizontalSpeed = 1.875F;
            const float airbornedHorizontalSpeed = 2.8125F;

            Body.Vertical.UseGravity = true;
            Body.Vertical.Speed = verticalSpeed;
            Body.Horizontal.Speed = GetFallbackHorizontalSpeed(airbornedHorizontalSpeed);

            yield return new WaitUntil(() => Body.IsAirborne);
            yield return new WaitUntil(() => Body.IsGrounded);

            Body.Horizontal.Speed = GetFallbackHorizontalSpeed(groundedHorizontalSpeed);
        }
    }
}