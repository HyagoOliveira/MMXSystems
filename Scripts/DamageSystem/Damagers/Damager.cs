using System;
using UnityEngine;

namespace MMX.DamageSystem
{
    /// <summary>
    /// Default damager implementation. It can damage up to 10 <see cref="Damageable"/> components.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class Damager : MonoBehaviour, IDamager
    {
        [field: SerializeField] public Collider2D Collider { get; set; }
        [field: SerializeField] public LayerMask Layers { get; set; }

        [field: Space]
        [field: SerializeField, Min(0F)] public float Amount { get; set; } = 1F;
        [field: SerializeField] public bool DisableAfterInflictDamage { get; set; }

        public event Action<Damageable> OnDamageInflicted;

        private readonly Collider[] buffer = new Collider[10];

        private void Reset() => Collider = GetComponent<Collider2D>();
        private void FixedUpdate() => TryInflictNearbyDamage();

        public void Enable() => SetEnable(true);
        public void Disable() => SetEnable(false);
        public void SetEnable(bool isEnabled) => gameObject.SetActive(isEnabled);

        public bool TryInflictDamage(Damageable damageable)
        {
            var wasDamageInflicted = damageable.TryTakeDamage(this);
            if (wasDamageInflicted)
            {
                OnDamageInflicted?.Invoke(damageable);
                if (DisableAfterInflictDamage) Disable();
            }
            return wasDamageInflicted;
        }

        private void TryInflictNearbyDamage()
        {
            var bounds = Collider.bounds;
            var hits = Physics.OverlapBoxNonAlloc(
                bounds.center,
                bounds.size * 0.5F,
                buffer,
                transform.rotation,
                Layers
            );

            for (var i = 0; i < hits; i++)
            {
                var hasDamageable = buffer[i].TryGetComponent(out Damageable damageable);
                if (hasDamageable) TryInflictDamage(damageable);
            }
        }
    }
}