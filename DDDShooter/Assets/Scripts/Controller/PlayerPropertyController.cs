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
                _selectedWeapon = -1;
                if (weaponIndex >= 0)
                {
                    Weapon weapon = _inventory.GetWeapon(weaponIndex);
                    if (weapon != null)
                    {
                        _weaponController.On(weapon);
                        _selectedWeapon = weaponIndex;
                    }                    
                }
            }
        }

        #endregion
    }
}
