using UnityEngine;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class GetReadyState : AbstractState
    {
        protected override void EnterState()
        {
            base.EnterState();
            Player.DisableInteractions();
        }

        protected override void ExitState()
        {
            base.ExitState();
            Player.EnableInteractions();
        }
    }
}