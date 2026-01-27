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
        public static event Action<HitType, Vector3, Vector3> OnHitPlaced;
        public static void PlaceHit(HitType hit, Transform place) => PlaceHit(hit, place.position, place.eulerAngles);
        public static void PlaceHit(HitType hit, Vector3 position, Vector3 rotation) => OnHitPlaced?.Invoke(hit, position, rotation);

        public static event Action<ExplosionType, Transform> OnExplosionPlaced;
        public static void PlaceExplosion(ExplosionType explosion, Transform place) => OnExplosionPlaced?.Invoke(explosion, place);
        #endregion
    }
}