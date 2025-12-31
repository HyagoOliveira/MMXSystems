using UnityEngine;
using MMX.CoreSystem;
using MMXD.LoadingSystem;

namespace MMX.PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerPack", menuName = "MMX/Players/Player Pack", order = 110)]
    public sealed class PlayerPack : AsyncPack<PlayerName>
    {
        public async Awaitable<Player[]> InstantiateAllAsync(Transform parent = null)
        {
            var i = 0;
            var players = new Player[Count];

            foreach (var playerName in GetTypes())
            {
                var instance = await InstantiateAsync(playerName, parent);
                var player = instance.GetComponent<Player>();
                players[i++] = player;
            }

            return players;
        }
    }
}