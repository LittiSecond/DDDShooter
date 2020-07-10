namespace Geekbrains
{
    public sealed class WeaponController : BaseController
    {
        #region Fields

        private Weapon _weapon;

        #endregion


        #region Methods

        public override void On(params BaseObjectScene[] weapon)
        {
            if (IsActive) return;
            if (weapon.Length > 0) _weapon = weapon[0] as Weapon;
            if (_weapon == null) return;
            base.On(_weapon);
            _weapon.IsVisible = true;
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _weapon.IsVisible = false;
            _weapon = null;
        }

        public void Fire()
        {
            if (IsActive)
            {
                _weapon.Fire();
            }
        }

        #endregion
    }
}
