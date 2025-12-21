using UnityEngine;
using MMX.InputSystem;

namespace MMX.PlayerSystem
{
    [DefaultExecutionOrder(-1)]
    [DisallowMultipleComponent]
    public sealed class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private Player player;

        private InputActions inputActions;
        private PlayerActions playerActions;

        private void Awake()
        {
            Initialize();
            SubscriveEvents();
        }

        private void OnEnable() => playerActions.Enable();
        private void Update() => playerActions.Update();
        private void OnDisable() => playerActions.Disable();

        private void OnDestroy()
        {
            UnsubscriveEvents();
            Dispose();
        }

        private void Initialize()
        {
            inputActions = new InputActions();
            playerActions = new PlayerActions(inputActions.Player);
        }

        private void SubscriveEvents()
        {
            playerActions.OnMoved += player.SetMoveInput;
            playerActions.OnJumped += player.SetJumpInput;
            playerActions.OnDashed += player.SetDashInput;
            playerActions.OnMainAttacked += player.SetMainAttackInput;
            playerActions.OnSideAttacked += player.SetSideAttackInput;
            playerActions.OnGigaAttacked += player.SetGigaAttackInput;
            playerActions.OnSwitched += player.SwitchInput;
        }

        private void UnsubscriveEvents()
        {
            playerActions.OnMoved -= player.SetMoveInput;
            playerActions.OnJumped -= player.SetJumpInput;
            playerActions.OnDashed -= player.SetDashInput;
            playerActions.OnMainAttacked -= player.SetMainAttackInput;
            playerActions.OnSideAttacked -= player.SetSideAttackInput;
            playerActions.OnGigaAttacked -= player.SetGigaAttackInput;
            playerActions.OnSwitched -= player.SwitchInput;
        }

        private void Dispose()
        {
            playerActions = null;
            inputActions.Dispose();
        }
    }
}