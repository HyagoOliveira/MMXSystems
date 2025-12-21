using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MMX.InputSystem
{
    /// <summary>
    /// Invokes an event when the GameObject is clicked.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider))]
    public sealed class ClickHandler : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClicked;

        public void OnPointerClick(PointerEventData _) => OnClicked?.Invoke();
    }
}
