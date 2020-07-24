using System;
using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class Health : BaseController, ITakerDamage
    {
        #region Fields

        private float _health;

        #endregion

        public event Action OnDeathEventHandler;

        #region Properties

        public float MaxHeath { get; private set; }

        public float CurrentHealth { get => _health; }

        #endregion

        
        #region ClassLifeCycles

        public Health(float maxHeath)
        {
            _health = MaxHeath = maxHeath;
        }

        #endregion


        #region Methods

        public void TakeDamage(float damag)
        {
            if (damag > 0 || _health > 0)
            {
                _health -= damag;
                if (_health <= 0)
                {
                    DestroyItself();
                }
            }
        }

        private void DestroyItself()
        {
            //if (_damagTranslators != null)
            //{
            //    foreach (var t in _damagTranslators)
            //    {
            //        t.Die();
            //    }
                OnDeathEventHandler?.Invoke();
            //}
        }

        #endregion

    }
}
