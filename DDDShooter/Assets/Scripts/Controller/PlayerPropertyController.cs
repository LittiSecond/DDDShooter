using Geekbrains;


namespace DddShooter
{
    public sealed class PlayerPropertyController : BaseController, IInitialization
    {
        //  PlayerPropertyManager ?
        #region Fields

        private WeaponController _weaponController;
        private Inventory _inventory;

        private int _selectedWeapon = -1;

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
            if (weaponIndex != _selectedWeapon)
            {
                _weaponController.Off();
                _selectedWeapon = weaponIndex;
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
            int nextWeapon = _selectedWeapon + 1;
            if (nextWeapon >= Inventory.WEAPON_SLOTS_QUANTITY)
            {
                nextWeapon = 0;
            }
            SelectWeapon(nextWeapon);
        }

        public void SelectPreviousWeapon()
        {
            int nextWeapon = _selectedWeapon - 1;
            if (nextWeapon < 0)
            {
                nextWeapon = Inventory.WEAPON_SLOTS_QUANTITY - 1;
            }
            SelectWeapon(nextWeapon);
        }

        #endregion
    }
}
