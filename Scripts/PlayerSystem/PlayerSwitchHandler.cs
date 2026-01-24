using UnityEngine;
using MMX.CoreSystem;
using ActionCode.SerializedDictionaries;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class PlayerSwitchHandler : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<PlayerName, GameObject> instances;

        private void Start() => TryFindPlayer();
        private void OnEnable() => PlayerManager.OnPlayerSwitched += HandlePlayerSwitched;
        private void OnDisable() => PlayerManager.OnPlayerSwitched -= HandlePlayerSwitched;

        private void HandlePlayerSwitched(Player player)
        {
            DisableAllPlayerImages();
            instances[player.Name].SetActive(true);
        }

        private void DisableAllPlayerImages()
        {
            foreach (var instance in instances.Values)
            {
                instance.SetActive(false);
            }
        }

        private void TryFindPlayer()
        {
            var player = FindAnyObjectByType<Player>();
            if (player) HandlePlayerSwitched(player);
        }
    }
}