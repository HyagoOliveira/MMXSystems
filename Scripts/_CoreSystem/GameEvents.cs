using System;
using UnityEngine;

namespace MMX.CoreSystem
{
    /// <summary>
    /// Global static class that holds all Game Events.
    /// </summary>
    public static class GameEvents
    {
        #region VISUAL EFFECTS
        public static event Action<HitType, Vector3> OnHitPlaced;
        public static void PlaceHit(HitType hit, Vector3 position) => OnHitPlaced?.Invoke(hit, position);
        #endregion
    }
}