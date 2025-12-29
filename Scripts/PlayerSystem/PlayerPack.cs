using UnityEngine;
using MMX.CoreSystem;
using MMXD.LoadingSystem;

namespace MMX.PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerPack", menuName = "MMX/Players/Player Pack", order = 110)]
    public sealed class PlayerPack : AsyncPack<PlayerName>
    {
    }
}