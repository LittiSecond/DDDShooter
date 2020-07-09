using System;
using UnityEngine;


namespace Geekbrains
{
    [Serializable]
    public sealed class SmallBattery
    {
        #region Fields

        [SerializeField] private float _chargeCurrent;
        [SerializeField] private float _chargeMax;
        private const float DEFOULT_CHARGE_MAX = 100.0f;

        #endregion


        #region Properties

        public float ChargeMax
        {
            get => _chargeMax;
        }

        public float ChargeCurrent
        {
            get => _chargeCurrent;
            set => _chargeCurrent = Mathf.Clamp(value, 0, _chargeMax);
        }

        #endregion


        #region ClassLifeCycles

        public SmallBattery()
        {
            _chargeCurrent = _chargeMax = DEFOULT_CHARGE_MAX;
        }

        public SmallBattery(float chargeMax)
        {
            _chargeCurrent = _chargeMax = chargeMax;
        }

        #endregion

    }
}
