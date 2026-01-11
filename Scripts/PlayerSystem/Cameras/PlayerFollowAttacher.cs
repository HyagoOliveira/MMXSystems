using UnityEngine;
using ActionCode.Cinemachine;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(FollowAttacher))]
    public sealed class PlayerFollowAttacher : MonoBehaviour
    {
        [SerializeField] private FollowAttacher follower;

        private void Reset() => follower = GetComponent<FollowAttacher>();
        private void OnEnable() => SubscribeEvents();
        private void OnDisable() => UnsubscribeEvents();

        private void SubscribeEvents()
        {
            PlayerManager.OnPlayerSpawned += HandlePlayerSpawned;
            PlayerManager.OnPlayerUnSpawned += HandlePlayerUnSpawned;
        }

        private void UnsubscribeEvents()
        {
            PlayerManager.OnPlayerSpawned -= HandlePlayerSpawned;
            PlayerManager.OnPlayerUnSpawned -= HandlePlayerUnSpawned;
        }

        private void HandlePlayerSpawned(Player player) => follower.Attach(player.transform);
        private void HandlePlayerUnSpawned(Player _) => follower.Detach();
    }
}