using UnityEngine;
using System.Collections;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class NormalHurtState : AbstractHurtState
    {
        [Header("Fallback")]
        [SerializeField] private float decrement = 0.8F;
        [SerializeField] private float initialSpeed = 8.68F;

        public static readonly WaitForFixedUpdate waitOneFrame = new();

        protected override void ApplyTrigger() => Player.Animator.NormalHurt();

        protected override IEnumerator Fallback()
        {
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