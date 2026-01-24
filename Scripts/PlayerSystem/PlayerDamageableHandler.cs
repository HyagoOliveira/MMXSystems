using UnityEngine;
using ActionCode.EnergySystem;
using ActionCode.Sidescroller;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class PlayerDamageableHandler : MonoBehaviour
    {
        [SerializeField] private Player player;

        private void Reset() => player = GetComponent<Player>();
        private void Awake() => OverrideGetDamageFunction();
        private void FixedUpdate() => CheckDeathCollisions();

        private void OnEnable()
        {
            player.Energy.OnEnergyEnded += HandleEnergyEnded;
            player.Damageable.OnDamageTaken += HandleDamaged;
        }

        private void OnDisable()
        {
            player.Energy.OnEnergyEnded -= HandleEnergyEnded;
            player.Damageable.OnDamageTaken -= HandleDamaged;
        }

        private void CheckDeathCollisions()
        {
            var isDead =
                player.Damageable.IsAbleToReceiveDamage() &&
                player.Motor.IsTileCollision(TileType.Death);
            //TODO: check crush death too
            if (isDead) player.Energy.RemoveAll();
        }

        private void HandleEnergyEnded() => player.Kill();

        private void HandleDamaged(Damager damager)
        {
            if (!player.IsStucked()) TriggerHurt(damager.Amount);
            RotateTowards(damager.transform.position.x);
        }

        private void TriggerHurt(float amount)
        {
            const int bigHurtDamageThreshold = 2;

            var isBigHurtDamage = amount > bigHurtDamageThreshold;
            AbstractHurtState damageState = isBigHurtDamage ? player.BigHurt : player.NormalHurt;

            damageState.Trigger();
        }

        private void RotateTowards(float horizontalPosition) => player.Motor.RotateHorizontally(horizontalPosition);
        private void OverrideGetDamageFunction() => player.Damageable.getDamage = GetDamageUsingArmor;
        private float GetDamageUsingArmor(Damager damager) => player.IsUsingAnyArmor ? GetArmoredDamage(damager.Amount) : damager.Amount;

        private static float GetArmoredDamage(float damage)
        {
            var d = Mathf.Floor(damage) - 1F;
            return System.Math.Max(d, 1F); // should be from System.Math
        }
    }
}