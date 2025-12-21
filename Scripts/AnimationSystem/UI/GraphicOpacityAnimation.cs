using UnityEngine;
using UnityEngine.UI;

namespace MMX.AnimationSystem
{
    [DisallowMultipleComponent]
    public sealed class GraphicOpacityAnimation : AbstractAnimation
    {
        [SerializeField] private Graphic graphic;
        [Tooltip("Curve to evaluate opacity over time (0f to 1f).")]
        public AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        private void Reset()
        {
            graphic = GetComponent<Graphic>();
            curve.postWrapMode = WrapMode.Loop;
        }

        public async Awaitable PlayAsync(AnimationCurve curve)
        {
            Stop();
            var time = curve.keys[^1].time;

            while (CurrentTime < time)
            {
                UpdateCurrentTime();
                UpdateAnimation();
                await Awaitable.NextFrameAsync();
            }
        }

        protected override void UpdateAnimation()
        {
            var color = graphic.color;
            color.a = curve.Evaluate(CurrentTime);
            graphic.color = color;
        }
    }
}
