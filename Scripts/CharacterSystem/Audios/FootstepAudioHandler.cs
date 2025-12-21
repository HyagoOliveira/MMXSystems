using UnityEngine;

namespace MMX.CharacterSystem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public sealed class FootstepAudioHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AnimationEvents events;

        private void Reset()
        {
            audioSource = GetComponent<AudioSource>();
            events = GetComponentInParent<AnimationEvents>();
        }

        private void OnEnable() => events.OnFootstep += HandleFootstep;
        private void OnDisable() => events.OnFootstep -= HandleFootstep;

        private void HandleFootstep() => audioSource.Play();
    }
}