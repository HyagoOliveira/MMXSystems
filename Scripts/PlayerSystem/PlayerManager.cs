using System;
using UnityEngine;
using System.Linq;
using MMX.CoreSystem;
using MMX.CharacterSystem;
using System.Collections.Generic;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class PlayerManager : MonoBehaviour
    {
        public PlayerName first;
        [SerializeField] private PlayerPack pack;

        public Player Current => players[currentName];
        public Transform LastSpawnPlace { get; private set; }
        public static PlayerManager Instance { get; private set; }

        public static event Action<PlayerName> OnPlayerSpawned;

        private Dictionary<PlayerName, Player> players;
        private PlayerName currentName = PlayerName.None;

        private void Awake()
        {
            Instance = this;

            FindPlayers();
            FindFirst();
            UpdateSpawnPlace();
            Current.Place(LastSpawnPlace);
        }

        private void OnDestroy() => Instance = null;

        public void Spawn(PlayerName player)
        {
            currentName = player;

            Current.Spawn(LastSpawnPlace);
            OnPlayerSpawned?.Invoke(currentName);
        }

        public void UpdateSpawnPlace() => LastSpawnPlace = Place.Find("SpawnPlace");

        private async void FindPlayers()
        {
            var scenePlayers = FindObjectsByType<Player>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.InstanceID
            );
            players = scenePlayers.ToDictionary(p => p.Name, p => p);

            var hasAllPlayers = players.Count == pack.Count;
            if (hasAllPlayers) return;

            var parent = GetPlayerParent();

            foreach (var playerName in pack.AvailablePlayers)
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
            if (hasFirstPlayer) currentName = currentPlayer.Name;
        }

        private static Transform GetPlayerParent()
        {
            var parent = GameObject.Find("Players");
            return parent != null ? parent.transform : null;
        }
    }
}