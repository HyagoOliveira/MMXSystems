using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using MMX.CoreSystem;
using MMX.CharacterSystem;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class PlayerManager : MonoBehaviour
    {
        public PlayerName first;
        [SerializeField] private PlayerPack pack;

        [Header("Switching")]
        [SerializeField, Min(0f), Tooltip("The time (in seconds) to wait until switch Players again.")]
        private float switchTime = 2F;

        public Player Current => players[currentName];
        public bool IsSwitching { get; private set; }
        public Transform LastSpawnPlace { get; private set; }

        public static event Action<PlayerName> OnPlayerSpawned;
        public static event Action<PlayerName> OnPlayerSwitched;
        public static event Action<PlayerName> OnPlayerUnSpawned;

        private static PlayerManager Instance { get; set; }

        private Dictionary<PlayerName, Player> players;
        private PlayerName currentName = PlayerName.None;

        private void Awake()
        {
            Instance = this;

            FindPlayers();
            FindFirst();
            SortPlayers();
            UpdateSpawnPlace();

            Current.Place(LastSpawnPlace);
        }

        private void OnDestroy() => Instance = null;

        public static void Spawn(PlayerName player)
        {
            if (Instance) Instance.Spawn_Internal(player);
        }

        public static void UnSpawn()
        {
            if (Instance) Instance.UnSpawn_Internal();
        }

        public static void Switch(PlayerName player)
        {
            if (Instance) Instance.Switch_Internal(player);
        }

        public static void SwitchToNext()
        {
            if (Instance) Instance.SwitchToNext_Internal();
        }

        private void Spawn_Internal(PlayerName player)
        {
            currentName = player;

            Current.Spawn(LastSpawnPlace);
            OnPlayerSpawned?.Invoke(currentName);
        }

        private void UnSpawn_Internal()
        {
            Current.UnSpawn();
            OnPlayerUnSpawned?.Invoke(Current.Name);
        }

        private void SwitchToNext_Internal() => Switch_Internal(GetNextPlayerName());

        private async void Switch_Internal(PlayerName player)
        {
            var isAbleToSwitch =
                !IsSwitching &&
                Current.IsAbleToSwitchOut() &&
                IsAbleToSwitchFor(player);

            if (!isAbleToSwitch) return;

            IsSwitching = true;

            await SwitchAsync(player);
            await Awaitable.WaitForSecondsAsync(switchTime);

            IsSwitching = false;
        }

        private async Awaitable SwitchAsync(PlayerName player)
        {
            Current.transform.GetPositionAndRotation(out var position, out var rotation);
            UnSpawn_Internal();

            await Awaitable.NextFrameAsync(); // Waits to enter in UnSpawn State.
            await Awaitable.WaitForSecondsAsync(1f);
            //await Current.GetOut.WaitWhileIsExecutingAsync();

            currentName = player;
            Current.Switch(position, rotation);

            OnPlayerSwitched?.Invoke(Current.Name);
        }

        private void UpdateSpawnPlace() => LastSpawnPlace = Place.Find("SpawnPlace");

        public bool IsAbleToSwitchFor(PlayerName playerName) =>
            players.TryGetValue(playerName, out Player player) &&
            player.IsAbleToSwitchIn();

        private PlayerName GetNextPlayerName()
        {
            var index = Current.Order;
            if (++index >= players.Count) index = 0;
            return players.Keys.ElementAt(index);
        }

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

        private void SortPlayers()
        {
            var index = 0;
            foreach (var player in players.Values)
            {
                player.Order = index++;
                player.Energy.CompleteToIntial(); //TODO improve EnergySystem
            }
        }

        private static Transform GetPlayerParent()
        {
            var parent = GameObject.Find("Players");
            return parent != null ? parent.transform : null;
        }
    }
}