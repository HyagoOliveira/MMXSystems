using UnityEngine;
using MMX.VisualEffectSystem;

namespace MMX.StageSystem
{
    public class StageVisualEffectsHandler : MonoBehaviour
    {
        [SerializeField] private HitPoolSystemDictionary hits;
        [SerializeField] private ExplosionPoolSystemDictionary explosions;

        private void OnEnable()
        {
            VisualEffectManager.OnHitPlaced += HandleHitPlaced;
            VisualEffectManager.OnExplosionPlaced += HandleExplosionPlaced;
        }

        private void OnDisable()
        {
            VisualEffectManager.OnHitPlaced -= HandleHitPlaced;
            VisualEffectManager.OnExplosionPlaced -= HandleExplosionPlaced;
        }

        private void HandleHitPlaced(HitType hit, Vector3 position, Vector3 rotation) =>
            hits.Place(hit, position, rotation);
        private void HandleExplosionPlaced(ExplosionType explosion, Vector3 position, Vector3 rotation) =>
            explosions.Place(explosion, position, rotation);
    }
}