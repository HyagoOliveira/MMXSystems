using UnityEngine;

namespace MMX.AnimationSystem
{
    [RequireComponent(typeof(RectTransform))]
    public sealed class RectPositionAnimation : AbstractAnimation
    {
        [SerializeField] private RectTransform rect;

        [Space]
        public Vector3AnimationCurve curve;

        private Vector3 initialPosition;

        private void Reset()
        {
            curve.Reset(yCurveAmplitude: 10f);
            rect = GetComponent<RectTransform>();
        }

        private void Awake() => initialPosition = rect.localPosition;
        protected override void UpdateAnimation() => rect.localPosition = initialPosition + curve.Evaluate(CurrentTime);
    }
}
