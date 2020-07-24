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
            throw new NotImplementedException();
        }

        #endregion

    }
}
