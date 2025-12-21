using UnityEngine;
using MMX.CoreSystem;
using MMX.WeaponSystem;

namespace MMX.PlayerSystem
{
    [DisallowMultipleComponent]
    public abstract class AbstractArmor : MonoBehaviour
    {
        [field: SerializeField] public ArmorName Name { get; private set; }

        [field: Space]
        [field: SerializeField] public AbstractWeapon MainWeapon { get; private set; }
        [field: SerializeField] public AbstractWeapon SideWeapon { get; private set; }
        [field: SerializeField] public AbstractWeapon GigaWeapon { get; private set; }

        public void SetMainWeaponInput(bool hasInput) { if (MainWeapon) MainWeapon.SetInput(hasInput); }
        public void SetSideWeaponInput(bool hasInput) { if (SideWeapon) SideWeapon.SetInput(hasInput); }
        public void SetGigaWeaponInput(bool hasInput) { if (GigaWeapon) GigaWeapon.SetInput(hasInput); }
    }
}