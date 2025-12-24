using UnityEngine;
using MMX.CoreSystem;
using MMX.CharacterSystem;
using ActionCode.Physics;
using ActionCode.Sidescroller;
using ActionCode.EnergySystem;
using ActionCode.AnimatorStates;
using ActionCode.ColliderAdapter;

namespace MMX.PlayerSystem
{
    /// <summary>
    /// Player main component.
    /// All player main components should be accessed through this component.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Motor))]
    [RequireComponent(typeof(Energy))]
    [RequireComponent(typeof(BoxBody))]
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(AnimationEvents))]
    [RequireComponent(typeof(AnimatorStateMachine))]
    [RequireComponent(typeof(BoxCollider2DAdapter))]
    public sealed class Player : MonoBehaviour
    {
        [field: SerializeField] public PlayerName Name { get; private set; } = PlayerName.None;
        [field: SerializeField] public Motor Motor { get; private set; }
        [field: SerializeField] public BoxBody Body { get; private set; }
        [field: SerializeField] public AnimationEvents Events { get; private set; }
        [field: SerializeField] public PlayerAnimator Animator { get; private set; }
        [field: SerializeField] public AnimatorStateMachine StateMachine { get; private set; }
        [field: SerializeField] public BoxCollider2DAdapter ColliderAdapter { get; private set; }

        [field: Header("Health")]
        [field: SerializeField] public Energy Energy { get; private set; }
        [field: SerializeField] public Damageable Damageable { get; private set; }

        // Player should not references PlayerHandlers (PlayerAnimationHandler, PlayerInputHandler, etc)
        // PlayerHandlers should access the Player component and handler everything necessary from there.

        public AbstractArmor CurrentArmor { get; private set; }
        public BoxCollider2D Collider => ColliderAdapter.Collider;

        #region States
        /*public RayInState RayIn => StateMachine.GetState<RayInState>();
        public GetOutState GetOut => StateMachine.GetState<GetOutState>();
        public IdleState Idle => StateMachine.GetState<IdleState>();*/
        public JumpState Jump => StateMachine.GetState<JumpState>();
        public FallState Fall => StateMachine.GetState<FallState>();
        //public StartDashState StartDash => StateMachine.GetState<StartDashState>();
        public DashState Dash => StateMachine.GetState<DashState>();
        /*public EndDashState EndDash => StateMachine.GetState<EndDashState>();
        public LandState Land => StateMachine.GetState<LandState>();*/
        public CrouchState Crouch => StateMachine.GetState<CrouchState>();
        public WallGrabState WallGrab => StateMachine.GetState<WallGrabState>();
        public WallSlideState WallSlide => StateMachine.GetState<WallSlideState>();
        public WallJumpState WallJump => StateMachine.GetState<WallJumpState>();
        public ClimbLadderState ClimbLadder => StateMachine.GetState<ClimbLadderState>();
        /*public ZiplineLocomotionState ZiplineLocomotion => StateMachine.GetState<ZiplineLocomotionState>();
        public NormalHurtState NormalHurt => StateMachine.GetState<NormalHurtState>();
        public BigHurtState BigHurt => StateMachine.GetState<BigHurtState>();
        public DeathState Death => StateMachine.GetState<DeathState>();
        public StuckState Stuck => StateMachine.GetState<StuckState>();*/
        #endregion

        private AbstractArmorLoader armorLoader;

        private void Reset()
        {
            Motor = GetComponent<Motor>();
            Body = GetComponent<BoxBody>();
            Events = GetComponent<AnimationEvents>();
            Animator = GetComponent<PlayerAnimator>();
            StateMachine = GetComponent<AnimatorStateMachine>();
            ColliderAdapter = GetComponent<BoxCollider2DAdapter>();

            Energy = GetComponent<Energy>();
            Damageable = GetComponent<Damageable>();
        }

        private void Awake()
        {
            CurrentArmor = GetComponentInChildren<AbstractArmor>();
            armorLoader = GetComponentInChildren<AbstractArmorLoader>();
        }

        //TODO load armor from GameData

        public async void LoadArmor(ArmorName armor) => CurrentArmor = await armorLoader.LoadAsync(armor);

        public void Enable() => gameObject.SetActive(true);
        public void Disable() => gameObject.SetActive(false);

        public void EnableInteractions() { }
        public void DisableInteractions() { }

        #region Inputs
        public void SetMoveInput(Vector2 input)
        {
            Motor.SetMoveInput(input);
            //ZiplineLocomotion.SetMoveInput(input);

            Crouch.SetInput(input.y);
            //ClimbLadder.SetInput(input.y);
        }

        public void SetJumpInput(bool hasInput)
        {
            Jump.Input.Set(hasInput);
            //WallJump.SetInput(hasInput);
        }

        public void SetDashInput(bool hasInput) { }// => Dash.Input.Set(hasInput);
        public void SetMainAttackInput(bool hasInput) => CurrentArmor.SetMainWeaponInput(hasInput);
        public void SetSideAttackInput(bool hasInput) => CurrentArmor.SetSideWeaponInput(hasInput);
        public void SetGigaAttackInput(bool hasInput) => CurrentArmor.SetGigaWeaponInput(hasInput);
        public void SwitchInput() { }
        #endregion
    }
}