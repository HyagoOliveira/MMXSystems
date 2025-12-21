using UnityEngine;

namespace MMX.MenuSystem
{
    /// <summary>
    /// Abstract controller for any menu component.
    /// </summary>
    public abstract class AbstractController : MonoBehaviour
    {
        public bool IsEnabled => gameObject.activeInHierarchy;

        public virtual void Focus() { }

        public string GetIdentifier() => GetType().Name;
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public virtual async Awaitable LoadAnyContentAsync() => await Awaitable.NextFrameAsync();
    }
}