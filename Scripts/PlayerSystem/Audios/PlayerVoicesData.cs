using UnityEngine;

namespace MMX.PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerVoicesData", menuName = "MMX/Players/Voices")]
    public sealed class PlayerVoicesData : ScriptableObject
    {
        [Header("Jumps")]
        public AudioClip wallJump;
        public AudioClip airJump;
        public AudioClip[] groundJumps;

        [Header("Attacks")]
        public AudioClip busterAttack;
        public AudioClip[] saberAttacks;

        [Header("Health")]
        public AudioClip death;
        public AudioClip lowEnergy;
        public AudioClip[] hurts;

        public AudioClip GetRandomJumpShout()
        {
            var index = Random.Range(0, groundJumps.Length);
            return groundJumps[index];
        }

        public AudioClip GetRandomHurtShout()
        {
            var index = Random.Range(0, hurts.Length);
            return hurts[index];
        }

        public bool TryGetSaberAttack(int index, out AudioClip shout)
        {
            var hasAttack = index >= 0 && index < saberAttacks.Length;
            shout = hasAttack ? saberAttacks[index] : null;
            return hasAttack;
        }
    }
}