using UnityEngine;

namespace MMX.MenuSystem
{
    [DisallowMultipleComponent]
    public sealed class AnyButtonScreen : AbstractScreen
    {
        [Header("Transition")]
        [SerializeField, Tooltip("Whether can go back to this screen from Main Menu.")]
        private bool canGoBack;
        [SerializeField, Tooltip("The next menu screen to open.")]
        private string nextScreenName = "MainMenuScreen";

        [Header("Labels")]
        [SerializeField, Tooltip("The Label to show when idle.")]
        private GameObject idle;
        [SerializeField, Tooltip("The Label to show when clicked.")]
        private GameObject clicked;
    }
}
