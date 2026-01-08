using UnityEngine;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class PlayerAudioHandler : MonoBehaviour
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource headSource;
        [SerializeField] private AudioSource bodySource;
        [SerializeField] private AudioSource bootsSource;
        [SerializeField] private AudioSource dashMiddleSource;

        private PlayerSoundsData Sounds => player.Sounds;

        private Player player;

        private void Awake()
        {
            player = GetComponentInParent<Player>();
            dashMiddleSource.clip = Sounds.common.dashMiddle;
        }

        private void OnEnable()
        {
            //player.Death.OnEntered += HandleDeathEntered;
            player.GetOut.OnEntered += HandleGetOutEntered;
            //player.BigHurt.OnEntered += HandleHurtEntered;
            //player.NormalHurt.OnEntered += HandleHurtEntered;
            player.RayIn.OnEntered += HandleRayInEntered;
            //player.Stuck.OnEntered += HandleHurtEntered;

            player.StartDash.OnEntered += HandleStartDashEntered;
            player.Dash.OnEntered += HandleDashEntered;
            player.Dash.OnExited += HandleDashExited;
            player.EndDash.OnEntered += HandleEndDashEntered;

            //player.WallGrab.OnEntered += HandleWallGrabEntered;
            //player.WallJump.OnEntered += HandleWallJumpEntered;

            player.Jump.OnEntered += HandleJumpEntered;
            player.Land.OnEntered += HandleLandEntered;

            player.AnimationEvents.OnVictory += HandleGetReadied;
            player.AnimationEvents.OnGetReadied += HandleGetReadied;
        }

        private void OnDisable()
        {
            //player.Death.OnEntered -= HandleDeathEntered;
            player.GetOut.OnEntered -= HandleGetOutEntered;
            //player.BigHurt.OnEntered -= HandleHurtEntered;
            //player.NormalHurt.OnEntered -= HandleHurtEntered;
            player.RayIn.OnEntered -= HandleRayInEntered;
            //player.Stuck.OnEntered -= HandleHurtEntered;

            player.StartDash.OnEntered -= HandleStartDashEntered;
            player.Dash.OnEntered -= HandleDashEntered;
            player.Dash.OnExited -= HandleDashExited;
            player.EndDash.OnEntered -= HandleEndDashEntered;

            //player.WallGrab.OnEntered -= HandleWallGrabEntered;
            //player.WallJump.OnEntered -= HandleWallJumpEntered;

            player.Jump.OnEntered -= HandleJumpEntered;
            player.Land.OnEntered -= HandleLandEntered;

            player.AnimationEvents.OnVictory -= HandleGetReadied;
            player.AnimationEvents.OnGetReadied -= HandleGetReadied;
        }

        private void HandleDeathEntered() => PlayDeath();
        private void HandleGetOutEntered() => PlayRayOut();
        private void HandleHurtEntered() => PlayRandomHurtShout();
        private void HandleRayInEntered() => PlayRayEnter();
        private void HandleGetReadied() => headSource.PlayOneShot(Sounds.common.victory);

        public void PlayRayEnter() => bodySource.PlayOneShot(Sounds.rayEnter);
        public void PlayRayOut() => bodySource.PlayOneShot(Sounds.rayOut);

        public void PlayStartDash() => bootsSource.PlayOneShot(Sounds.common.dashStart);
        public void PlayEndDash() => bootsSource.PlayOneShot(Sounds.common.dashEnd);
        public void PlayMiddleDash() => dashMiddleSource.Play();
        public void StopMiddleDash() => dashMiddleSource.Stop();

        public void PlayDeath() => headSource.PlayOneShot(Sounds.voice.death);
        public void PlayLowEnergy() => headSource.PlayOneShot(Sounds.voice.lowEnergy);
        public void PlayRandomHurtShout() => headSource.PlayOneShot(Sounds.voice.GetRandomHurtShout());

        public void TryPlayBusterChargedAttackShout()
        {
            var playAttack = Random.value < 0.5F;
            if (playAttack) PlayBusterAttack();
        }

        public void TryPlaySaberAttackShout(int attackIndex)
        {
            var hasShout = Sounds.voice.TryGetSaberAttack(attackIndex, out AudioClip shout);
            if (hasShout) headSource.PlayOneShot(shout);
        }

        private void HandleStartDashEntered() => PlayStartDash();
        private void HandleDashEntered() => PlayMiddleDash();

        private void HandleDashExited()
        {
            if (player.Dash.WasStartedFromGround)
                StopMiddleDash();
        }

        private void HandleEndDashEntered()
        {
            if (player.Body.IsGrounded) PlayEndDash();
        }

        private void HandleWallGrabEntered() => PlayWallGrab();
        private void HandleWallJumpEntered() => PlayWallJumpShout();

        private void HandleJumpEntered()
        {
            PlayBootsJump();
            PlayJumpShout();
        }

        private void HandleLandEntered() => PlayLand();

        private void PlayBootsJump() => bootsSource.PlayOneShot(Sounds.jump);

        private void PlayJumpShout()
        {
            var shout = player.Jump.WasAirJump ?
                Sounds.voice.airJump :
                Sounds.voice.GetRandomJumpShout();

            headSource.PlayOneShot(shout);
        }

        private void PlayWallGrab() => bodySource.PlayOneShot(Sounds.wallGrab);
        private void PlayWallJumpShout() => headSource.PlayOneShot(Sounds.voice.wallJump);

        private void PlayLand() => bootsSource.PlayOneShot(Sounds.land);
        private void PlayBusterAttack() => headSource.PlayOneShot(Sounds.voice.busterAttack);
    }
}