using UnityEngine;

namespace MMX.CharacterSystem
{
    [DisallowMultipleComponent]
    public sealed class Place : MonoBehaviour
    {
        public static Transform Find(string name = "Place")
        {
            var place = FindAnyObjectByType<Place>(FindObjectsInactive.Exclude);
            if (place == null)
            {
                var go = new GameObject(name);
                place = go.AddComponent<Place>();
            }
            return place.transform;
        }
    }
}