﻿using System;

namespace DddShooter
{
    public sealed class EnemyHealth
    {
        #region Fields

        private float _maxHealth;
        private float _heath;
        
        #endregion
        

        public event Action OnDeathEventHandler;


        #region ClassLifeCycles

        public EnemyHealth(NpcSettings settings)
        {
            if (settings != null)
            {
                _heath = _maxHealth = settings.MaxHealth;
            }
        }

        #endregion


        #region Methods

        public void TakeDamage(float damag)
        {
            if (damag > 0 || _heath > 0)
            {
                _heath -= damag;
                if (_heath <= 0)
                {
                    DestroyItself();
                }
            }
        }

        private void DestroyItself()
        {
                OnDeathEventHandler?.Invoke();
        }

        #endregion


    }
}
