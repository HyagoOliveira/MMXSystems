using UnityEngine;

namespace MMX.PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerSoundsData", menuName = "MMX/Players/Sounds")]
    public sealed class PlayerSoundsData : ScriptableObject
    {
        public PlayerVoicesData voice;
        public PlayerCommonSoundsData common;

        [Header("Spawn")]
        public AudioClip rayEnter;
        public AudioClip rayOut;

        [Header("Jumps")]
        public AudioClip jump;
        public AudioClip land;

        [Header("Wall")]
        public AudioClip wallGrab;
    }
}