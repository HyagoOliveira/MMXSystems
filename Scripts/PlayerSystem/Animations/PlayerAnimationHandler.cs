using UnityEngine;
using ActionCode.Sidescroller;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Player))]
    public sealed class PlayerAnimationHandler : MonoBehaviour
    {
        [SerializeField] private Player player;

        private Motor Motor => player.Motor;
        private PlayerAnimator Animator => player.Animator;

        private void Reset() => player = GetComponent<Player>();
        private void Start() => UpdateMotor();
        private void Update() => UpdateMotor();

        private void UpdateMotor()
        {
            Animator.IsHurting = Motor.IsHurting;
            Animator.IsFalling = Motor.IsFalling();
            Animator.IsGrounded = Motor.Body.IsGrounded;
            Animator.IsAirborne = Motor.Body.IsAirborne;
            Animator.IsPushingWall = Motor.IsPushingWall();
            Animator.IsFacingCollision = Motor.IsFacingCollision();
            Animator.HasHorizontalInput = Motor.HasInputHorizontal();
            Animator.HasAnyDirectionalInput = Motor.HasInputHorizontal() || Motor.HasInputDown();
        }
    }
}