using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

namespace MMX.InputSystem
{
    public sealed class PlayerActions
    {
        public event Action<Vector2> OnMoved;
        public event Action<bool> OnJumped;
        public event Action<bool> OnDashed;
        public event Action<bool> OnMainAttacked;
        public event Action<bool> OnSideAttacked;
        public event Action<bool> OnGigaAttacked;
        public event Action OnSwitched;
        public event Action OnIdleSwitched;

        private readonly InputActions.PlayerActions actions;
        private RebindingOperation rebindingOperation;

        public PlayerActions(InputActions.PlayerActions actions) =>
            this.actions = actions;

        public void Enable() => actions.Enable();
        public void Disable() => actions.Disable();

        public void Rebind(InputAction action)
        {
            rebindingOperation?.Dispose();

            action.Disable();

            rebindingOperation = action.PerformInteractiveRebinding().OnComplete(HandleRebindCompleted);
            rebindingOperation.Start();
        }

        public void Update()
        {
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPaused) UpdateUsingLegacySystem();
            else UpdateUsingNewSystem();
#else
            UpdateUsingNewSystem();
#endif
        }

        private void HandleRebindCompleted(RebindingOperation operation)
        {
            rebindingOperation.Dispose();
            operation.action.Enable();
        }

        private void UpdateUsingNewSystem()
        {
            var moveAxis = actions.Move.ReadValue<Vector2>();
            var jumpButton = actions.Jump.IsPressed();
            var dashButton = actions.Dash.IsPressed();
            var mainAttackButton = actions.MainAttack.IsPressed();
            var sideAttackButton = actions.SideAttack.IsPressed();
            var gigaAttackButton = actions.GigaAttack.IsPressed();
            var switchButton = actions.Switch.IsPressed();
            var idleSwitchButton = actions.SwitchIdle.IsPressed();

            UpdateInputs(
                moveAxis,
                jumpButton,
                dashButton,
                mainAttackButton,
                sideAttackButton,
                gigaAttackButton,
                switchButton,
                idleSwitchButton
            );
        }

        private void UpdateInputs(
            Vector2 moveAxis,
            bool jumpButton,
            bool dashButton,
            bool mainAttackButton,
            bool sideAttackButton,
            bool gigaAttackButton,
            bool switchButton,
            bool idleSwitchButton
        )
        {
            OnMoved?.Invoke(moveAxis);
            OnJumped?.Invoke(jumpButton);
            OnDashed?.Invoke(dashButton);
            OnMainAttacked?.Invoke(mainAttackButton);
            OnSideAttacked?.Invoke(sideAttackButton);
            OnGigaAttacked?.Invoke(gigaAttackButton);

            if (switchButton) OnSwitched?.Invoke();
            if (idleSwitchButton) OnIdleSwitched?.Invoke();
        }

        #region Legacy System
        private void UpdateUsingLegacySystem()
        {
            var moveAxis = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            );
            var jumpButton = Input.GetButton("Jump");
            var dashButton = Input.GetButton("Dash");
            var mainAttackButton = Input.GetButton("MainAttack");
            var sideAttackButton = Input.GetButton("SideAttack");
            var gigaAttackButton = Input.GetButton("GigaAttack");
            var idleSwitchButton = Input.GetButton("SwitchIdle");
            var switchButton = GetSwitchPlayerAxisDown();


            UpdateInputs(
                moveAxis,
                jumpButton,
                dashButton,
                mainAttackButton,
                sideAttackButton,
                gigaAttackButton,
                switchButton,
                idleSwitchButton
            );
        }

        private bool wasSwitchPlayerDown;
        private bool GetSwitchPlayerAxisDown()
        {
            var isSwitchPlayer = Input.GetAxisRaw("SwitchPlayer") > 0f;
            var isSwitchPlayerDown = !wasSwitchPlayerDown && isSwitchPlayer;
            wasSwitchPlayerDown = isSwitchPlayer;

            return isSwitchPlayerDown;
        }
        #endregion
    }
}