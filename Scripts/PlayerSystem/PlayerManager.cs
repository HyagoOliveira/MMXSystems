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
        [SerializeField] private PlayerPack pack;

        [Header("Switching")]
        [SerializeField, Min(0f), Tooltip("The time (in seconds) to wait until switch Players again.")]
        private float switchTime = 2F;

        public Player Current => players[currentName];
        public bool IsSwitching { get; private set; }
        public Transform LastSpawnPlace { get; private set; }

        public static event Action<Player> OnPlayerSpawned;
        public static event Action<Player> OnPlayerUnSpawned;
        public static event Action<Player> OnPlayerSwitched;
        //public static event Action<Player> OnPlayerKilled;

        private static PlayerManager Instance { get; set; }

        private Dictionary<PlayerName, Player> players;
        private PlayerName currentName = PlayerName.None;

        private static readonly Quaternion rightRotation = Quaternion.identity;
        private static readonly Quaternion leftRotation = Quaternion.Euler(Vector3.up * 180f);

        private async void Awake()
        {
            Instance = this;

            await FindPlayers();
            FindFirst();
            InitPlayers();
            UpdateSpawnPlace();
        }

        private void OnDestroy() => Instance = null;

        public static void Spawn(PlayerName player, Vector3 position, Quaternion rotation)
        {
            if (Instance) Instance.Spawn_Internal(player, position, rotation);
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

        public static void Kill()
        {
            if (Instance) Instance.Kill_Internal();
        }

        public static bool IsCollidingWithCurrentPlayer(Bounds bounds) =>
            Instance.Current.Body.Collider.Bounds.Intersects(bounds);

        public static Player GetCurrentPlayer() => Instance.Current;

        public static float GetDistanceFromCurrentPlayer(Vector3 position) =>
            Mathf.Abs(Vector2.Distance(Instance.Current.Center.position, position));

        public static Vector3 GetDirectionFromCurrentPlayer(Vector3 position)
        {
            var delta = Instance.Current.Center.position - position;
            return delta.normalized;
        }

        public static Quaternion GetRotation(float horizontalPosition)
        {
            var isPlayerLeft = Instance.Current.Center.position.x < horizontalPosition;
            return isPlayerLeft ? leftRotation : rightRotation;
        }

        private void Spawn_Internal(PlayerName player, Vector3 position, Quaternion rotation)
        {
            currentName = player;

            Current.Spawn(position, rotation);
            OnPlayerSpawned?.Invoke(Current);
        }

        private void UnSpawn_Internal()
        {
            Current.UnSpawn();
            OnPlayerUnSpawned?.Invoke(Current);
        }

        private void Kill_Internal()
        {
            Current.Kill();
            //OnPlayerKilled?.Invoke(Current);
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
            await Current.GetOut.WaitWhileIsExecutingAsync();

            Spawn_Internal(player, position, rotation);
            OnPlayerSwitched?.Invoke(Current);
        }

        private void UpdateSpawnPlace() => LastSpawnPlace = Place.Find("SpawnPlace");

        public bool IsAbleToSwitchFor(PlayerName playerName) =>
            players.TryGetValue(playerName, out Player player) &&
            player.IsAbleToSwitchIn();

        private PlayerName GetNextPlayerName() => pack.GetNextAvailablePlayerName(Current.Name);

        private async Awaitable FindPlayers()
        {
            players = GetScenePlayers();
            var hasAllPlayers = players.Count == pack.Count;
            if (!hasAllPlayers) await InstantiateAvailablePlayers();
        }

        private async Awaitable InstantiateAvailablePlayers()
        {
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

        private void FindFirst() => currentName = pack.AvailablePlayers[0];

        private void InitPlayers()
        {
            foreach (var player in players.Values)
            {
                player.Energy.CompleteToIntial(); //TODO improve EnergySystem
            }
        }

        private static Transform GetPlayerParent()
        {
            var parent = GameObject.Find("Players");
            return parent != null ? parent.transform : null;
        }

        private static Dictionary<PlayerName, Player> GetScenePlayers()
        {
            var players = new Dictionary<PlayerName, Player>();
            var scenePlayers = FindObjectsByType<Player>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

            try
            {
                // May have duplicate keys
                players = scenePlayers.ToDictionary(p => p.Name, p => p);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return players;
        }
    }
}