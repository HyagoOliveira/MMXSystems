using UnityEngine;

namespace MMX.MenuSystem
{
    /// <summary>
    /// Abstract Menu Screen. 
    /// Menu Screens are used to display different parts of a Menu, as a sub-menu or a specific section.
    /// </summary>
	public abstract class AbstractScreen : AbstractController
    {
        public Menu Menu { get; private set; }

        public virtual void Initialize(Menu menu) => Menu = menu;

        public virtual Awaitable FadeInAsync() => Awaitable.EndOfFrameAsync();
        public virtual Awaitable FadeOutAsync() => Awaitable.EndOfFrameAsync();
    }
}