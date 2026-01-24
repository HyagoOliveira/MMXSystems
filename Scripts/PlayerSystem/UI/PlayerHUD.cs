using UnityEngine;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class PlayerHUD : MonoBehaviour
    {
        [field: SerializeField] public PlayerEnergyBarHandler EnergyBar { get; private set; }
        //TODO add more things

        private void Start() => CheckCurrentPlayer();
        private void OnEnable() => PlayerManager.OnPlayerSpawned += HandlePlayerSpawned;
        private void OnDisable() => PlayerManager.OnPlayerSpawned -= HandlePlayerSpawned;

        private void HandlePlayerSpawned(Player _) => EnergyBar.Show();

        private void CheckCurrentPlayer()
        {
            if (PlayerManager.HasCurrentPlayer()) EnergyBar.Show();
            else EnergyBar.Hide();
        }
    }
}