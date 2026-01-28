using System;
using UnityEngine;

namespace MMX.VisualEffectSystem
{
    /// <summary>
    /// Global manager for handling visual effects like hits and explosions.
    /// </summary>
    public static class VisualEffectManager
    {
        public static event Action<HitType, Vector3, Vector3> OnHitPlaced;
        public static event Action<ExplosionType, Vector3, Vector3> OnExplosionPlaced;

        public static void PlaceHit(HitType hit, Transform place) => PlaceHit(hit, place.position, place.eulerAngles);
        public static void PlaceHit(HitType hit, Vector3 position, Vector3 rotation) => OnHitPlaced?.Invoke(hit, position, rotation);

        public static void PlaceExplosion(ExplosionType explosion, Transform place) => PlaceExplosion(explosion, place.position, place.eulerAngles);
        public static void PlaceExplosion(ExplosionType explosion, Vector3 position, Vector3 rotation) =>
            OnExplosionPlaced?.Invoke(explosion, position, rotation);
    }
}