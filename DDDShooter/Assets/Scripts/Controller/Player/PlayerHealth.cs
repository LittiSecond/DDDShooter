using System;
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

        public event Action OnDeathEventHandler;

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
                        OnDeathEventHandler?.Invoke();
                        Off();
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

        public override void On(params BaseObjectScene[] body)
        {
            if (IsActive) return;
            if (body == null) return;
            if (body.Length == 0) return;
            _body = body[0] as PlayerBody;
            if (!_body) return;

            _heath = _maxHealth;
            _halthIndicator.SetValue(_heath, _maxHealth);
            _body.OnDamagedEvent += TakeDamage;
            _body.OnHealingEvent += TakeHealing;
            base.On(body);
        }

        public override void Off()
        {
            if (IsActive)
            {
                base.Off();
                _body.OnDamagedEvent -= TakeDamage;
                _body.OnHealingEvent -= TakeHealing;
                _body = null;
            }
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            _halthIndicator = ServiceLocatorMonoBehaviour.GetService<UiPlayerHalthIndicator>(false);
        }

        #endregion
    }
}
