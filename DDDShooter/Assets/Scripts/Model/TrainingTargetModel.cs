using System;
using UnityEngine;


namespace Geekbrains
{
    public sealed class TrainingTargetModel : BaseObjectScene, ITakerDamage
    {
        #region Fields

        private UiTrainingTarget _uiTrainingTarget;

        private float _fullDamage;
        private float _lastDamage;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _uiTrainingTarget = GetComponentInChildren<UiTrainingTarget>();
        }

        #endregion


        #region Methods

        #endregion


        #region ITakerDamage

        public void TakeDamage(float damag)
        {
            _lastDamage = damag;
            _fullDamage += damag;
            _uiTrainingTarget.SetDamagInfo(_lastDamage, _fullDamage);
        }

        #endregion

    }
}
