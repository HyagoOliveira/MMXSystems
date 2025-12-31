using UnityEngine;

namespace MMX.CharacterSystem
{
    [DisallowMultipleComponent]
    public sealed class Place : MonoBehaviour
    {
        public static Transform Find()
        {
            var place = FindAnyObjectByType<Place>();
            if (place == null)
            {
                var go = new GameObject("Place");
                place = go.AddComponent<Place>();
            }
            return place.transform;
        }
    }
}