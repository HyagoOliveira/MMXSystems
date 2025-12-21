using UnityEngine;

namespace MMX.AnimationSystem
{
    [DisallowMultipleComponent]
    public sealed class RotationAnimation : AbstractAnimation
    {
        public Vector3 angle = Vector3.up * -15f;

        protected override void UpdateAnimation() => transform.Rotate(angle * GetSpeedPerSecond());
    }
}
