using UnityEngine;

namespace MMX.PlayerSystem
{
    public abstract class AbstractState : ActionCode.Sidescroller.AbstractState
    {
        [SerializeField] private Player player;

        public Player Player => player;

        protected override void Reset()
        {
            base.Reset();
            player = GetRequiredComponent<Player>();
        }
    }
}