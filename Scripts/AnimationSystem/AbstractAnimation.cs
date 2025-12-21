using UnityEngine;

namespace MMX.AnimationSystem
{
    public abstract class AbstractAnimation : MonoBehaviour
    {
        public bool useUnscaledTime;
        public float speed = 1f;

        public float CurrentTime { get; private set; }

        private void OnEnable() => ResetTime();

        private void Update()
        {
            UpdateCurrentTime();
            UpdateAnimation();
        }

        public void ResetTime() => CurrentTime = 0f;

        public void Restart()
        {
            ResetTime();
            Play();
        }

        public void Play() => enabled = true;
        public void Pause() => enabled = false;

        public void Stop()
        {
            Pause();
            CurrentTime = 0f;
        }

        public float GetDeltaTime() => useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
        public float GetSpeedPerSecond() => GetDeltaTime() * speed;

        protected abstract void UpdateAnimation();
        protected void UpdateCurrentTime() => CurrentTime += GetSpeedPerSecond();
    }
}