using MMX.CoreSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class PlayerManager : MonoBehaviour
    {
        public PlayerName first;
        [SerializeField] private PlayerPack pack;

        public Player Current { get; private set; }

        private Dictionary<PlayerName, Player> players;

        private void Awake()
        {
            FindPlayers();
            FindFirst();
        }

        private async void FindPlayers()
        {
            var instances = FindObjectsByType<Player>(FindObjectsSortMode.InstanceID);
            players = instances.ToDictionary(p => p.Name, p => p);
            var hasAllPlayers = players.Count == pack.Count;
            if (hasAllPlayers) return;

            var parent = GetPlayerParent();

            foreach (var playerName in pack.GetTypes())
            {
                var hasPlayer = players.ContainsKey(playerName);
                if (hasPlayer) continue;

                var instance = await pack.InstantiateAsync(playerName, parent);
                var player = instance.GetComponent<Player>();
                player.Disable();

                players.Add(playerName, player);
            }
        }

        private void FindFirst()
        {
            var hasFirstPlayer = players.TryGetValue(first, out var currentPlayer);
            if (hasFirstPlayer) Current = currentPlayer;
        }

        private static Transform GetPlayerParent()
        {
            var parent = GameObject.Find("Players");
            return parent != null ? parent.transform : null;
        }
    }
}
