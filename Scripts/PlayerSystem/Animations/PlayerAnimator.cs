using UnityEngine;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Player))]
    public sealed class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Player player;

        private Animator Animator => player.StateMachine.Animator;

        #region Param Properties
        public float IdleIndex
        {
            get => Animator.GetFloat(idleIndex);
            internal set => Animator.SetFloat(idleIndex, value);
        }

        public bool IsRay
        {
            get => Animator.GetBool(isRay);
            internal set => Animator.SetBool(isRay, value);
        }

        public bool IsGrounded
        {
            get => Animator.GetBool(isGrounded);
            internal set => Animator.SetBool(isGrounded, value);
        }

        public bool IsAirborne
        {
            get => Animator.GetBool(isAirborne);
            internal set => Animator.SetBool(isAirborne, value);
        }

        public bool IsFalling
        {
            get => Animator.GetBool(isFalling);
            internal set => Animator.SetBool(isFalling, value);
        }

        public bool IsHurting
        {
            get => Animator.GetBool(isHurting);
            internal set => Animator.SetBool(isHurting, value);
        }

        public bool IsDashing
        {
            get => Animator.GetBool(isDashing);
            internal set => Animator.SetBool(isDashing, value);
        }

        public bool IsPushingWall
        {
            get => Animator.GetBool(isPushingWall);
            internal set => Animator.SetBool(isPushingWall, value);
        }

        public bool IsFacingCollision
        {
            get => Animator.GetBool(isFacingCollision);
            internal set => Animator.SetBool(isFacingCollision, value);
        }

        public bool HasHorizontalInput
        {
            get => Animator.GetBool(hasHorizontalInput);
            internal set => Animator.SetBool(hasHorizontalInput, value);
        }
        #endregion

        #region Hashes
        private static readonly int idleIndex = Animator.StringToHash("IdleIndex");

        private static readonly int isRay = Animator.StringToHash("IsRay");
        private static readonly int isGrounded = Animator.StringToHash("IsGrounded");
        private static readonly int isAirborne = Animator.StringToHash("IsAirborne");
        private static readonly int isFalling = Animator.StringToHash("IsFalling");
        private static readonly int isHurting = Animator.StringToHash("IsHurting");
        private static readonly int isDashing = Animator.StringToHash("IsDashing");
        private static readonly int isPushingWall = Animator.StringToHash("IsPushingWall");
        private static readonly int isFacingCollision = Animator.StringToHash("IsFacingCollision");
        private static readonly int hasHorizontalInput = Animator.StringToHash("HasHorizontalInput");

        private static readonly int win = Animator.StringToHash("Win");
        private static readonly int spawn = Animator.StringToHash("Spawn");
        private static readonly int getOut = Animator.StringToHash("GetOut");
        private static readonly int @switch = Animator.StringToHash("Switch");

        private static readonly string weaponLayerName = "Weapon";

        #region State Names
        private static readonly int wallSliding = Animator.StringToHash("Wall Sliding");
        private static readonly int ziplineLocomotion = Animator.StringToHash("Zipline Locomotion");
        private static readonly int saberWallAttack = Animator.StringToHash("Saber Wall Attack");
        private static readonly int saberZiplineAttack = Animator.StringToHash("Saber Zipline Attack");
        #endregion

        #endregion

        private int weaponLayerIndex;

        private void Reset() => player = GetComponent<Player>();
        private void Awake() => FindLayers();

        public bool IsExecutingAnyValidState() => !IsExecutingAnyLockedState();
        public bool IsWallSlidingState() => player.StateMachine.IsAnimatorState(wallSliding);
        public bool IsZiplineLocomotion() => player.StateMachine.IsAnimatorState(ziplineLocomotion);
        public bool IsSaberWallAttackState() => player.StateMachine.IsAnimatorState(saberWallAttack);
        public bool IsSaberZiplineAttack() => player.StateMachine.IsAnimatorState(saberZiplineAttack);

        /// <summary>
        /// Locked States are states where cannot exit without finish it completely.
        /// </summary>
        /// <returns></returns>
        public bool IsExecutingAnyLockedState() => true;
        /*player.StateMachine.IsExecuting<DeathState>() ||
        player.StateMachine.IsExecuting<StuckState>() ||
        player.StateMachine.IsExecuting<BigHurtState>() ||
        player.StateMachine.IsExecuting<NormalHurtState>() ||
        player.StateMachine.IsExecuting<ZiplineAttachState>() ||
        player.StateMachine.IsExecuting<ClimbLadderUpState>() ||
        player.StateMachine.IsExecuting<ClimbLadderDownState>() ||
        player.StateMachine.IsExecuting<GetLadderUpState>() ||
        player.StateMachine.IsExecuting<GetLadderDownState>() ||
        player.StateMachine.IsExecuting<SaberLadderAttackState>() ||
        player.StateMachine.IsExecuting<BusterLadderAttackState>() ||
        player.StateMachine.IsExecuting<Zero.ChargedBusterAttackState>();*/


        #region Triggers
        public void Win() => Animator.SetTrigger(win);
        public void Spawn() => Animator.SetTrigger(spawn);
        public void GetOut() => Animator.SetTrigger(getOut);
        public void Switch() => Animator.SetTrigger(@switch);
        #endregion

        #region Layers
        public void ToggleWeapon()
        {
            if (IsWeaponLowed()) RaiseWeapon();
            else if (IsWeaponRaised()) LowWeapon();
        }

        public void LowWeapon() => SetWeaponWeight(0f);
        public void RaiseWeapon() => SetWeaponWeight(1f);

        private void FindLayers() => weaponLayerIndex = Animator.GetLayerIndex(weaponLayerName);
        private void SetWeaponWeight(float weight) => Animator.SetLayerWeight(weaponLayerIndex, weight);

        private bool IsWeaponLowed() => GetWeaponWeight() == 0f;
        private bool IsWeaponRaised() => GetWeaponWeight() == 1f;
        private float GetWeaponWeight() => Animator.GetLayerWeight(weaponLayerIndex);
        #endregion
    }
}