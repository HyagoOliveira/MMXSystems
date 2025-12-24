using UnityEngine;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class IdleState : AbstractState
    {
        private const int mainIndex = 0;
        private const int sideIndex = 1; //TODO add side idle animation into MMXD
        private const int damagedIndex = 2;

        protected override void EnterState()
        {
            base.EnterState();
            Body.Horizontal.StopSpeed();
        }

        public void ToggleBetweenMainAndSideIdle()
        {
            if (IsMainIdle()) EnableSideIdle();
            else if (IsSideIdle()) EnableMainIdle();
        }

        public void EnableMainIdle() => Player.Animator.IdleIndex = mainIndex;
        public void EnableSideIdle() => Player.Animator.IdleIndex = sideIndex;
        public void EnableDamagedIdle() => Player.Animator.IdleIndex = damagedIndex;

        public bool IsMainIdle() => Player.Animator.IdleIndex == mainIndex;
        public bool IsSideIdle() => Player.Animator.IdleIndex == sideIndex;
        public bool IsDamagedIdle() => Player.Animator.IdleIndex == damagedIndex;
    }
}