using System;
using UnityEngine;


namespace DddShooter
{
    [Serializable]
    public sealed class Clip
    {
        #region Fields

        [SerializeField] private AmmunitionType _type;

        [SerializeField] private int _capacity;
        [SerializeField] private int _quantity;

        private const int DEFOULT_CAPACITY = 12;

        #endregion


        #region Properties

        public int Capacity
        {
            get => _capacity;
        }

        public int Quantity
        {
            get => _quantity;
            //set => _quantity = Mathf.Clamp(value, 0, _capacity);
        }

        public AmmunitionType Type
        {
            get => _type;
        }

        #endregion


        #region ClassLifeCycles

        public Clip(AmmunitionType type, int capacity) 
        {
            _type = type;
            _quantity = _capacity = capacity;
        }

        public Clip(Clip other)
        {
            _type = other._type;
            _capacity = other._capacity;
            _quantity = other._quantity;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Extract one bullet
        /// </summary>
        /// <returns> false - cannot extract, clip is empty </returns>
        public bool Extract()
        {
            if (_quantity == 0)
            {
                return false;
            }
            else
            {
                _quantity--;
                return true;
            }
        }

        #endregion
    }
}
