using UnityEngine;
using ActionCode.EnergySystem;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(EnergyBar))]
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class PlayerEnergyBarHandler : MonoBehaviour
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private EnergyBar energyBar;

        private const float fadeTime = 0.25f;

        private void Reset()
        {
            group = GetComponent<CanvasGroup>();
            energyBar = GetComponent<EnergyBar>();
        }

        private void Start() => TryFindPlayer();

        private void OnEnable()
        {
            PlayerManager.OnPlayerSpawned += HandlePlayerEnergy;
            PlayerManager.OnPlayerSwitched += HandlePlayerEnergy;
        }

        private void OnDisable()
        {
            PlayerManager.OnPlayerSpawned -= HandlePlayerEnergy;
            PlayerManager.OnPlayerSwitched -= HandlePlayerEnergy;
        }

        public void Show() => _ = FadeAsync(group, 0f, 1f, fadeTime);
        public void Hide() => _ = FadeAsync(group, 1f, 0f, fadeTime);

        private void HandlePlayerEnergy(Player player) => energyBar.Energy = player.Energy;

        private void TryFindPlayer()
        {
            if (energyBar.Energy) return;
            var player = FindAnyObjectByType<Player>();
            if (player) HandlePlayerEnergy(player);
        }

        private async Awaitable FadeAsync(CanvasGroup group, float initial, float final, float duration)
        {
            var time = 0f;

            while (time < duration)
            {
                var step = time / duration;
                group.alpha = Mathf.Lerp(initial, final, step);

                time += Time.deltaTime;
                await Awaitable.NextFrameAsync();
            }

            group.alpha = final;
        }
    }
}