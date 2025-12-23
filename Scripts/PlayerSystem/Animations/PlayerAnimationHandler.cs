using UnityEngine;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Player))]
    public sealed class PlayerAnimationHandler : MonoBehaviour
    {
        [SerializeField] private Player player;

        public Animator Animator => player.StateMachine.Animator;

        #region Param Properties
        public int IdleIndex
        {
            get => Animator.GetInteger(idleIndex);
            internal set => Animator.SetInteger(idleIndex, value);
        }

        public bool IsRunning
        {
            get => Animator.GetBool(isRunning);
            internal set => Animator.SetBool(isRunning, value);
        }

        public bool IsDashing
        {
            get => Animator.GetBool(isDashing);
            internal set => Animator.SetBool(isDashing, value);
        }

        public bool IsGrounded
        {
            get => Animator.GetBool(isGrounded);
            internal set => Animator.SetBool(isGrounded, value);
        }

        public bool IsCrouching
        {
            get => Animator.GetBool(isCrouching);
            internal set => Animator.SetBool(isCrouching, value);
        }
        #endregion

        #region Hashes
        private static readonly int idleIndex = Animator.StringToHash("IdleIndex");

        private static readonly int isGrounded = Animator.StringToHash("IsGrounded");
        private static readonly int isAirborne = Animator.StringToHash("IsAirborne");
        private static readonly int isFalling = Animator.StringToHash("IsFalling");
        private static readonly int isHurting = Animator.StringToHash("IsHurting");
        private static readonly int isRunning = Animator.StringToHash("IsRunning");
        private static readonly int isDashing = Animator.StringToHash("IsDashing");
        private static readonly int isCrouching = Animator.StringToHash("IsCrouching");
        private static readonly int isPushingWall = Animator.StringToHash("IsPushingWall");
        private static readonly int isFacingCollision = Animator.StringToHash("IsFacingCollision");
        private static readonly int hasHorizontalInput = Animator.StringToHash("HasHorizontalInput");

        private static readonly int win = Animator.StringToHash("Win");
        private static readonly int @switch = Animator.StringToHash("Switch");

        private static readonly string weaponLayerName = "Weapon";
        #endregion

        private int weaponLayerIndex;

        private void Reset() => player = GetComponent<Player>();
        private void Awake() => FindLayers();
        private void Start() => UpdateMotor();
        private void Update() => UpdateMotor();

        #region Triggers
        public void Win() => Animator.SetTrigger(win);
        public void Switch() => Animator.SetTrigger(@switch);
        #endregion

        #region Layers
        public void LowWeapon() => SetWeaponWeight(0f);
        public void RaiseWeapon() => SetWeaponWeight(1f);

        private void FindLayers() => weaponLayerIndex = Animator.GetLayerIndex(weaponLayerName);
        private void SetWeaponWeight(float weight) => Animator.SetLayerWeight(weaponLayerIndex, weight);
        #endregion

        private void UpdateMotor()
        {
            Animator.SetBool(isFalling, player.Motor.IsFalling());
            Animator.SetBool(isGrounded, player.Motor.Body.IsGrounded);
            Animator.SetBool(isAirborne, player.Motor.Body.IsAirborne);
            Animator.SetBool(isHurting, player.Motor.IsHurting);
            Animator.SetBool(isPushingWall, player.Motor.IsPushingWall());
            Animator.SetBool(isFacingCollision, player.Motor.IsFacingCollision());
            Animator.SetBool(hasHorizontalInput, player.Motor.HasInputHorizontal());
        }
    }
}