using UnityEngine;

namespace MMX.AnimationSystem
{
    [System.Serializable]
    public struct Vector3AnimationCurve
    {
        public AnimationCurve x;
        public AnimationCurve y;
        public AnimationCurve z;

        public void Reset(float yCurveAmplitude = 1f)
        {
            x = AnimationCurve.Linear(0f, 0f, 1f, 0f);
            y = AnimationCurveUtility.Sine(time: 4f, yCurveAmplitude);
            z = AnimationCurve.Linear(0f, 0f, 1f, 0f);
        }

        public readonly Vector3 Evaluate(float time) => new(
            x.Evaluate(time),
            y.Evaluate(time),
            z.Evaluate(time)
        );
    }
}