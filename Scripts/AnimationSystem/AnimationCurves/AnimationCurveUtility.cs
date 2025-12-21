using UnityEngine;

namespace MMX.AnimationSystem
{
    public static class AnimationCurveUtility
    {
        /// <summary>
        /// Creates an AnimationCurve that follows a sine wave of one complete cycle using 5 keyframes.
        /// </summary>
        /// <remarks>
        /// Values: (0, amplitude, 0, -amplitude, 0)
        /// </remarks>
        /// <param name="time">The total curve time in seconds.</param>
        /// <param name="amplitude">The max/min Y value.</param>
        /// <returns></returns>
        public static AnimationCurve Sine(float time = 1f, float amplitude = 1f)
        {
            var keys = new Keyframe[]
            {
                new (0f, 0f),
                new (time * 0.25f, amplitude),
                new (time * 0.50f, 0f),
                new (time * 0.75f, -amplitude),
                new (time, 0f)
            };
            var curve = new AnimationCurve(keys) { postWrapMode = WrapMode.Loop };

            // Smooth tangents for all keyframes to create a smoothly sine wave.
            for (int i = 0; i < curve.keys.Length; i++)
            {
                curve.SmoothTangents(i, 0f);
            }

            return curve;
        }
    }
}