using UnityEngine;

namespace MMX.AnimationSystem
{
    [DisallowMultipleComponent]
    public sealed class PositionAnimation : AbstractAnimation
    {
        [Space]
        public Vector3AnimationCurve curve;

        private Vector3 initialPosition;

        private void Reset() => curve.Reset();
        private void Awake() => initialPosition = transform.localPosition;

        [ContextMenu("Teste")]
        void Teste()
        {
            foreach (var key in curve.y.keys)
            {
                print($"in: {key.inWeight}, out: {key.outWeight}");
            }
        }

        protected override void UpdateAnimation() => transform.localPosition = initialPosition + curve.Evaluate(CurrentTime);
    }
}