using System;
using Geekbrains;


namespace DddShooter
{
    public sealed class PlayerPropertyController : BaseController, IInitialization
    {
        // Управляет имуществом персонажа игрока. 

        #region Fields

        private UiWarningMessageText _warningMessageText;
        private WeaponController _weaponController;
        private Inventory _inventory;

        private int _selectedWeaponIndex = -1;

        private const int CANNOT_PICK_UP_WEAPON_TEXT_ID = 6;

        #endregion


        #region ClassLifeCycles

        #endregion


        #region Methods

        /// <summary>
        /// -1 - hide weapon
        /// </summary>
        /// <param name="weaponIndex"></param>
        public void SelectWeapon(int weaponIndex)
        {
            if (!IsActive)
            {
                return;
            }

            if (weaponIndex != _selectedWeaponIndex)
            {
                _weaponController.Off();
                _selectedWeaponIndex = weaponIndex;
                if (weaponIndex >= 0)
                {
                    Weapon weapon = _inventory.GetWeapon(weaponIndex);
                    if (weapon != null)
                    {
                        _weaponController.On(weapon);
                    }                    
                }
            }
        }

        public void SelectNextWeapon()
        {
            if (!IsActive)
            {
                return;
            }

            int nextWeapon = _selectedWeaponIndex + 1;
            if (nextWeapon >= Inventory.WEAPON_SLOTS_QUANTITY)
            {
                nextWeapon = 0;
            }
            SelectWeapon(nextWeapon);
        }

        public void SelectPreviousWeapon()
        {
            if (!IsActive)
            {
                return;
            }

            int nextWeapon = _selectedWeaponIndex - 1;
            if (nextWeapon < 0)
            {
                nextWeapon = Inventory.WEAPON_SLOTS_QUANTITY - 1;
            }
            SelectWeapon(nextWeapon);
        }

        /// <summary>
        /// Reload Weapon clip, if Weapon is selected, else reload FlashLight Battery. 
        /// </summary>
        public void Reload()
        {
            if (!IsActive)
            {
                return;
            }

            if (_weaponController.IsActive)
            {
                ReloadWeaponClip();
            }
            else
            {
                ReloadFlashLight();
            }
        }

        private void ReloadFlashLight()
        {
            if (IsActive)
            {
                SmallBattery battery = _inventory.GetSmallBattery();
                ServiceLocator.Resolve<FlashLightController>().ReplaceBattery(battery);                
            }

        }

        private void ReloadWeaponClip()
        {
            if (!IsActive)
            {
                return;
            }

            if (_weaponController.IsActive)
            {
                    AmmunitionType type = _weaponController.Type;
                    Clip newClip = _inventory.GetClip(type);
                    _weaponController.ReloadClip(newClip);

            }
        }

        public void DropItem()
        {
            if (!IsActive)
            {
                return;
            }

            if (_weaponController.IsActive)
            {
                _weaponController.Off();
                _inventory.DropWeapon(_selectedWeaponIndex);
                _selectedWeaponIndex = -1;
            }
        }

        public void PickUpWeapon(Weapon weapon)
        {
            if (!IsActive)
            {
                return;
            }

            if (weapon != null)
            {
                int index = _inventory.PickUpWeapon(weapon);
                if (index >= 0)
                {
                    if (_selectedWeaponIndex == index)
                    {
                        _selectedWeaponIndex = -1;
                    }
                    SelectWeapon(index);
                }
                else
                {
                    _warningMessageText.Show(CANNOT_PICK_UP_WEAPON_TEXT_ID);
                }
            }
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            _warningMessageText = UiInterface.WarningMessageText;
            _weaponController = ServiceLocator.Resolve<WeaponController>();
            _inventory = ServiceLocator.Resolve<Inventory>();
            //SelectWeapon(0);
        }

        #endregion
    }
}
