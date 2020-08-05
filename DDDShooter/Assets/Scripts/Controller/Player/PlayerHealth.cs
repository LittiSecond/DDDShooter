using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class PlayerHealth : BaseController, IInitialization
    {
        #region Fields

        private UiPlayerHalthIndicator _halthIndicator;
        private PlayerBody _body;

        private float _maxHealth = 60.0f;
        private float _heath;

        #endregion


        #region Methods

        private void TakeDamage(float damag)
        {
            if (IsActive)
            {
                if (damag > 0 || _heath > 0)
                {
                    _heath -= damag;
                    if (_heath <= 0)
                    {
                        _heath = 0;
                    }
                    UpdateUi();
                }
            }
        }

        private void UpdateUi()
        {
            _halthIndicator.SetValue(_heath);
        }

        private void TakeHealing(float healing)
        {
            if (IsActive)
            {
                if (healing > 0)
                {

                    _heath += healing;
                    if (_heath > _maxHealth)
                    {
                        _heath = _maxHealth;
                    }
                    UpdateUi();
                }
            }
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            _heath = _maxHealth;
            _halthIndicator = ServiceLocatorMonoBehaviour.GetService<UiPlayerHalthIndicator>(false);
            _halthIndicator.SetValue(_heath, _maxHealth);

            _body = ServiceLocatorMonoBehaviour.GetService<PlayerBody>(false);
            _body.OnDamagedEvent += TakeDamage;
            _body.OnHealingEvent += TakeHealing;
        }

        #endregion
    }
}
