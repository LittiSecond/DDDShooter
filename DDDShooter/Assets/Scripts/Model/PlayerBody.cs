using System;
using System.Collections.Generic;
using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    public sealed class PlayerBody : BaseObjectScene, ITakerDamage, ITakerHealing
    {
        #region Fields

        [SerializeField] private Transform _bodyCentre;

        public event Action<float> OnDamagedEvent = delegate { };
        public event Action<float> OnHealingEvent = delegate { };

        #endregion


        #region Properties

        public Transform BodyCentre { get => _bodyCentre; }

        #endregion

               
        #region ITakerDamage

        public void TakeDamage(float damag)
        {
            OnDamagedEvent.Invoke(damag);
        }

        #endregion


        #region ITakerHealing

        public void TakeHealing(float healing)
        {
            OnHealingEvent.Invoke(healing);
        }

        #endregion

    }
}
