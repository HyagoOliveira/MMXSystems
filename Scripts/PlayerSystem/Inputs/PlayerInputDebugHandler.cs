using UnityEngine;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public sealed class PlayerInputDebugHandler : MonoBehaviour
    {
        [SerializeField] private Player player;

        [Header("Keys")]
        [SerializeField] private KeyCode lowEnergy = KeyCode.L;
        [SerializeField] private KeyCode completeEnergy = KeyCode.K;
        [SerializeField] private KeyCode raiseWeapon = KeyCode.B;
        [SerializeField] private KeyCode invulnerability = KeyCode.I;
        //[SerializeField] private KeyCode nextWeapon = KeyCode.KeypadPlus;
        //[SerializeField] private KeyCode previousWeapon = KeyCode.KeypadMinus;

        private void Awake()
        {
            if (!Debug.isDebugBuild) enabled = false;
        }

        private void Update()
        {
            ToggleRaisedBuster();
            ToggleInvulnerability();

            //SelectArmor();
            //SelectWeapon();
            SelectEnergyAmount();
        }

        private void ToggleRaisedBuster()
        {
            if (Input.GetKeyDown(raiseWeapon))
                player.CurrentArmor.ToggleRaiseWeapon();
        }

        private void ToggleInvulnerability()
        {
            if (Input.GetKeyDown(invulnerability))
                player.Damageable.IsInvulnerable = !player.Damageable.IsInvulnerable;
        }

        /*private void SelectArmor()
        {
            const int initialKeypadDigit = (int)KeyCode.Keypad0;
            var finalKeypadDigit = initialKeypadDigit + player.Armors.TotalArmors;

            for (
                var (keypadIndex, armorIndex) = (initialKeypadDigit, 0);
                keypadIndex < finalKeypadDigit;
                keypadIndex++, armorIndex++
            )
            {
                var key = (KeyCode)keypadIndex;
                if (Input.GetKeyDown(key)) player.Armors.Equip(armorIndex);
            }
        }

        private void SelectWeapon()
        {
            if (Input.GetKeyDown(nextWeapon)) player.Weapons.Selector.Next();
            else if (Input.GetKeyDown(previousWeapon)) player.Weapons.Selector.Previous();
        }*/

        private void SelectEnergyAmount()
        {
            if (Input.GetKeyDown(lowEnergy))
            {
                var damage = player.Energy.Current - 1F;
                player.Energy.Remove(damage);
            }
            else if (Input.GetKeyDown(completeEnergy)) player.Energy.Complete();
        }
    }
}
