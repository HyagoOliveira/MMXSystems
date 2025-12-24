using UnityEngine;

namespace MMX.PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerCommonSoundSettings", menuName = "MMX/Players/Sounds")]
    public sealed class PlayerCommonSoundSettings : ScriptableObject
    {
        [Header("Dash")]
        public AudioClip dashStart;
        public AudioClip dashMiddle;
        public AudioClip dashEnd;

        [Header("Others")]
        public AudioClip death;
        public AudioClip footstep;
        public AudioClip freeze;
        public AudioClip victory;
        public AudioClip wallSlide;
    }
}