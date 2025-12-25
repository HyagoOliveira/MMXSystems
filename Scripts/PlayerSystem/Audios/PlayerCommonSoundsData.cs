using UnityEngine;

namespace MMX.PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerCommonSoundsData", menuName = "MMX/Players/Common Sounds")]
    public sealed class PlayerCommonSoundsData : ScriptableObject
    {
        [Header("Dash")]
        public AudioClip dashStart;
        public AudioClip dashMiddle;
        public AudioClip dashEnd;

        [Header("Others")]
        public AudioClip death;
        public AudioClip freeze;
        public AudioClip victory;
        public AudioClip wallSlide;
    }
}