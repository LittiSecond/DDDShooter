using DddShooter;


namespace Geekbrains
{
    public sealed class WeaponController : BaseController
    {
        #region Fields

        private Weapon _weapon;

        #endregion


        #region Properties

        public AmmunitionType Type
        {
            get
            {
                if (_weapon == null)
                {
                    return AmmunitionType.None;
                }
                else
                {
                    return _weapon.Type;
                }
            }
        }

        #endregion


        #region Methods

        public override void On(params BaseObjectScene[] weapon)
        {
            if (IsActive) return;
            if (weapon.Length > 0) _weapon = weapon[0] as Weapon;
            if (_weapon == null) return;
            base.On(_weapon);
            _weapon.IsVisible = true;

            UpdateUi();
            ShowUiClipInfo();
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _weapon.IsVisible = false;
            _weapon = null;

            HideUiClipInfo();
        }

        public void Fire()
        {
            if (IsActive)
            {
                _weapon.Fire();
                UpdateUi();
            }
        }

        public Clip ReloadClip(Clip newClip)
        {
            if (!IsActive)
            {
                return null;
            }
            Clip clip = _weapon.ReloadClip(newClip);
            UpdateUi();
            return clip;
        }

        private void UpdateUi()
        {
            int quantity = 0;
            int capasity = 0;
            Clip clip = _weapon.Clip;
            if (clip != null)
            {
                quantity = clip.Quantity;
                capasity = clip.Capacity;
            }
            UiInterface.UiClipInfoPanel.ShowData(quantity, capasity);
        }

        private void ShowUiClipInfo()
        {
            UiInterface.UiClipInfoPanel.Show();
        }

        private void HideUiClipInfo()
        {
            UiInterface.UiClipInfoPanel.Hide();
        }

        //private void ShowUiClipEmptyInfo()
        //{
        //    UiInterface.UiClipInfoPanel.ShowClipEmptyMessage();
        //}

        #endregion
    }
}
