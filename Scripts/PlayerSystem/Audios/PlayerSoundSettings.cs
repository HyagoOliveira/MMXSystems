using UnityEngine;

namespace MMX.PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerSoundSettings", menuName = "MMX/Players/Sounds")]
    public sealed class PlayerSoundSettings : ScriptableObject
    {
        public PlayerVoiceSettings voice;
        public PlayerCommonSoundSettings common;

        [Header("Spawn")]
        public AudioClip rayEnter;
        public AudioClip rayOut;
        public AudioClip victory;

        [Header("Jumps")]
        public AudioClip jump;
        public AudioClip land;

        [Header("Wall")]
        public AudioClip wallGrab;
    }
}