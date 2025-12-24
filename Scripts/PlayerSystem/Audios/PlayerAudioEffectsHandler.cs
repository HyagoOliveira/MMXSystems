using UnityEngine;

namespace MMX.PlayerSystem
{
    /// <summary>
    /// Handler for all Sounds Effects related to the Player.
    /// <para>Some public functions are called by Animation Events.</para>
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class PlayerAudioEffectsHandler : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private PlayerSoundsData sounds;

        [Header("Audio Sources")]
        [SerializeField] private AudioSource headSource;
        [SerializeField] private AudioSource bodySource;
        [SerializeField] private AudioSource bootsSource;
        [SerializeField] private AudioSource dashMiddleSource;

        private void Awake() => dashMiddleSource.clip = sounds.common.dashMiddle;

        private void OnEnable()
        {
            /*player.Death.OnEntered += HandleDeathEntered;
            player.GetOut.OnEntered += HandleGetOutEntered;
            player.BigHurt.OnEntered += HandleHurtEntered;
            player.NormalHurt.OnEntered += HandleHurtEntered;
            player.RayIn.OnEntered += HandleRayInEntered;
            player.Stuck.OnEntered += HandleHurtEntered;*/

            //player.StartDash.OnEntered += HandleStartDashEntered;
            player.Dash.OnEntered += HandleDashEntered;
            player.Dash.OnExited += HandleDashExited;
            //player.EndDash.OnEntered += HandleEndDashEntered;

            player.WallGrab.OnEntered += HandleWallGrabEntered;
            player.WallJump.OnEntered += HandleWallJumpEntered;

            player.Jump.OnEntered += HandleJumpEntered;
            player.Land.OnEntered += HandleLandEntered;
        }

        private void OnDisable()
        {
            /*player.Death.OnEntered -= HandleDeathEntered;
            player.GetOut.OnEntered -= HandleGetOutEntered;
            player.BigHurt.OnEntered -= HandleHurtEntered;
            player.NormalHurt.OnEntered -= HandleHurtEntered;
            player.RayIn.OnEntered -= HandleRayInEntered;
            player.Stuck.OnEntered -= HandleHurtEntered;*/

            //player.StartDash.OnEntered -= HandleStartDashEntered;
            player.Dash.OnEntered -= HandleDashEntered;
            player.Dash.OnExited -= HandleDashExited;
            //player.EndDash.OnEntered -= HandleEndDashEntered;

            player.WallGrab.OnEntered -= HandleWallGrabEntered;
            player.WallJump.OnEntered -= HandleWallJumpEntered;

            player.Jump.OnEntered -= HandleJumpEntered;
            player.Land.OnEntered -= HandleLandEntered;
        }

        private void HandleDeathEntered() => PlayDeath();
        private void HandleGetOutEntered() => PlayRayOut();
        private void HandleHurtEntered() => PlayRandomHurtShout();
        private void HandleRayInEntered() => PlayRayEnter();

        #region Animation Event Functions
        public void PlayVictory() => headSource.PlayOneShot(sounds.victory);
        #endregion

        public void PlayRayEnter() => bodySource.PlayOneShot(sounds.rayEnter);
        public void PlayRayOut() => bodySource.PlayOneShot(sounds.rayOut);

        public void PlayStartDash() => bootsSource.PlayOneShot(sounds.common.dashStart);
        public void PlayEndDash() => bootsSource.PlayOneShot(sounds.common.dashEnd);
        public void PlayMiddleDash() => dashMiddleSource.Play();
        public void StopMiddleDash() => dashMiddleSource.Stop();

        public void PlayDeath() => headSource.PlayOneShot(sounds.voice.death);
        public void PlayLowEnergy() => headSource.PlayOneShot(sounds.voice.lowEnergy);
        public void PlayRandomHurtShout() => headSource.PlayOneShot(sounds.voice.GetRandomHurtShout());

        public void TryPlayBusterChargedAttackShout()
        {
            var playAttack = Random.value < 0.5F;
            if (playAttack) PlayBusterAttack();
        }

        public void TryPlaySaberAttackShout(int attackIndex)
        {
            var hasShout = sounds.voice.TryGetSaberAttack(attackIndex, out AudioClip shout);
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

        private void PlayBootsJump() => bootsSource.PlayOneShot(sounds.jump);

        private void PlayJumpShout()
        {
            var shout = player.Jump.WasAirJump ?
                sounds.voice.airJump :
                sounds.voice.GetRandomJumpShout();

            headSource.PlayOneShot(shout);
        }

        private void PlayWallGrab() => bodySource.PlayOneShot(sounds.wallGrab);
        private void PlayWallJumpShout() => headSource.PlayOneShot(sounds.voice.wallJump);

        private void PlayLand() => bootsSource.PlayOneShot(sounds.land);
        private void PlayBusterAttack() => headSource.PlayOneShot(sounds.voice.busterAttack);
    }
}