using UnityEngine;

namespace MMX.WeaponSystem
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class AbstractWeapon : MonoBehaviour
    {
        [field: SerializeField] public AudioSource Audio { get; private set; }

        protected virtual void Reset() => Audio = GetComponent<AudioSource>();

        public void SetInput(bool hasInput)
        {
            if (hasInput) TryStartAttack();
            else ReleaseAttack();
        }

        public virtual bool CanAttack() => enabled;

        protected virtual void StartAttack() { }
        protected virtual void ReleaseAttack() { }

        private void TryStartAttack()
        {
            if (CanAttack()) StartAttack();
        }
    }
}