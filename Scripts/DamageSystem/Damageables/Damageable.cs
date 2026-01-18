using System;
using UnityEngine;

namespace MMX.DamageSystem
{
    /// <summary>
    /// Damageable component. It can be damaged by an <see cref="IDamager"/>.
    /// </summary>
    /// <remarks>
    /// It will use the <see cref="Energy"/> component when taking damage.
    /// If none is set, damages will be taken without removing any energy.
    /// </remarks>
    [DisallowMultipleComponent]
    public sealed class Damageable : MonoBehaviour
    {
        [field: SerializeField] public Energy Energy { get; set; }
        [field: SerializeField] public Collider2D Collider { get; set; }

        [field: Space]
        [field: SerializeField] public bool IsInvulnerable { get; set; }

        public event Action<IDamager> OnDamageTaken;

        private void Reset() => Energy = GetComponentInChildren<Energy>();

        public bool IsDestroyed() => !enabled;
        public bool IsAbleToTakeDamage() => enabled && !IsInvulnerable;

        public void Respawn()
        {
            enabled = true;
            Energy.CompleteToInitial();
        }

        public bool TryTakeDamage(IDamager damager)
        {
            var canTakeDamage = IsAbleToTakeDamage();
            if (canTakeDamage) TakeDamage(damager);
            return canTakeDamage;
        }

        public void TakeDamage(IDamager damager)
        {
            if (Energy)
            {
                var damage = damager.Amount;
                Energy.Remove(damage);

                if (Energy.IsEmpty())
                {
                    enabled = false;
                    return;
                }
            }

            OnDamageTaken?.Invoke(damager);
        }
    }
}