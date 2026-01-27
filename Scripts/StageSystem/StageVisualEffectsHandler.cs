using MMX.CoreSystem;
using UnityEngine;

namespace MMX.StageSystem
{
    public class StageVisualEffectsHandler : MonoBehaviour
    {
        [SerializeField] private HitPoolSystemDictionary hits;
        [SerializeField] private ExplosionPoolSystemDictionary explosions;

        private void OnEnable()
        {
            GameEvents.OnHitPlaced += HandleHitPlaced;
            GameEvents.OnExplosionPlaced += HandleExplosionPlaced;
        }

        private void OnDisable()
        {
            GameEvents.OnHitPlaced -= HandleHitPlaced;
            GameEvents.OnExplosionPlaced -= HandleExplosionPlaced;
        }

        private void HandleHitPlaced(HitType hit, Vector3 position, Vector3 rotation) => hits.Place(hit, position, rotation);
        private void HandleExplosionPlaced(ExplosionType explosion, Transform place) => explosions.Place(explosion, place);
    }
}