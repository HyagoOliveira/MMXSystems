using System;
using UnityEngine;
using ActionCode.PoolSystem;
using ActionCode.SerializedDictionaries;

namespace MMX.CoreSystem
{
    public abstract class AbstractPoolSystemDictionary<T> : MonoBehaviour where T : Enum
    {
        [SerializeField] private SerializedDictionary<T, PoolSystem> pools;

        public void Place(T type, Transform place) => Place(type, place.position, place.eulerAngles);
        public void Place(T type, Vector3 position, Vector3 rotation) => pools[type].Place(position, rotation);
        public void Place(T type, Vector3 position, Quaternion rotation) => pools[type].Place(position, rotation);
    }
}