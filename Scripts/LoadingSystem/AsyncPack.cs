using System;
using UnityEngine;
#if ADDRESSABLES
using UnityEngine.AddressableAssets;
#endif
using ActionCode.SerializedDictionaries;

namespace MMXD.LoadingSystem
{
    /// <summary>
    /// Generic class to load Prefabs asynchronously.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AsyncPack<T> : ScriptableObject where T : Enum
    {
#if ADDRESSABLES
        [SerializeField] private SerializedDictionary<T, AssetReferenceGameObject> prefabs;
#else
        [SerializeField] private SerializedDictionary<T, GameObject> prefabs;
#endif

        /// <summary>
        /// Asynchronously instantiates a GameObject using the given Prefab type. 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parent"></param>
        /// <returns>A GameObject instance.</returns>
        public async Awaitable<GameObject> InstantiateAsync(T type, Transform parent = null)
        {
            var prefab = prefabs[type];
#if ADDRESSABLES
            var operation = Addressables.LoadAssetAsync<GameObject>(prefab);
            var asset = await operation.Task;
            var instance = Instantiate(asset, parent);
#else
            var instance = Instantiate(prefab, parent);
#endif
            return instance;
        }

        public void Release(GameObject instance)
        {
#if ADDRESSABLES
            Addressables.ReleaseInstance(instance);
#else
            Destroy(instance);
#endif
        }
    }
}