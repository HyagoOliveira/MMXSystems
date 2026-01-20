using UnityEngine;
using System.Collections;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class NormalHurtState : AbstractHurtState
    {
        public static readonly WaitForFixedUpdate waitOneFrame = new();

        protected override void ApplyTrigger() => Player.Animator.NormalHurt();

        protected override IEnumerator Fallback()
        {
            const float decrement = 0.8F;
            const float initialSpeed = 8.68F;

            var speed = initialSpeed;

            do
            {
                Body.Horizontal.Speed = GetFallbackHorizontalSpeed(speed);

                yield return waitOneFrame;

                speed = Mathf.Max(speed - decrement, 0F);

            } while (Body.Horizontal.HasSpeed());
        }
    }
}