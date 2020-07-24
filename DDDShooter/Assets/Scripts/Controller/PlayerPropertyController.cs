using System;
using Geekbrains;


namespace DddShooter
{
    public sealed class PlayerPropertyController : BaseController, IInitialization
    {
        // Управляет имуществом персонажа игрока. ... наверно правильнее назвать  PlayerPropertyManager ?

        #region Fields

        private WeaponController _weaponController;
        private Inventory _inventory;

        private int _selectedWeaponIndex = -1;

        #endregion


        #region ClassLifeCycles

        #endregion


        #region Methods

        public void Initialization()
        {
            _weaponController = ServiceLocator.Resolve<WeaponController>();
            _inventory = ServiceLocator.Resolve<Inventory>();            
            SelectWeapon(0);
        }

        /// <summary>
        /// -1 - hide weapon
        /// </summary>
        /// <param name="weaponIndex"></param>
        public void SelectWeapon(int weaponIndex)
        {
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
            int nextWeapon = _selectedWeaponIndex + 1;
            if (nextWeapon >= Inventory.WEAPON_SLOTS_QUANTITY)
            {
                nextWeapon = 0;
            }
            SelectWeapon(nextWeapon);
        }

        public void SelectPreviousWeapon()
        {
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
            SmallBattery battery = _inventory.GetSmallBattery();
            ServiceLocator.Resolve<FlashLightController>().ReplaceBattery(battery);
        }

        private void ReloadWeaponClip()
        {
            if (_weaponController.IsActive)
            {
                AmmunitionType type = _weaponController.Type;
                Clip newClip = _inventory.GetClip(type);
                _weaponController.ReloadClip(newClip);
            }
            //if (_selectedWeaponIndex >= 0)
            //{
            //    Weapon weapon = _inventory.GetWeapon(_selectedWeaponIndex);
            //    if (weapon != null)
            //    {
            //        AmmunitionType type = weapon.Type;
            //        if (type != AmmunitionType.None)
            //        {
            //            if (weapon is Gun)
            //            {
            //                Gun gun = weapon as Gun;
            //                Clip newClip = _inventory.GetClip(type);
            //                gun.ReloadClip(newClip);
            //            }
            //        }

            //    }
            //}
        }

        #endregion
    }
}
