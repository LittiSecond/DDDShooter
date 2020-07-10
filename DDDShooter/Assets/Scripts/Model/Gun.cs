using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class Gun : Weapon
    {
        #region Fields

        [SerializeField] private Ammunition _ammunitionPrefab;
        [SerializeField] private float _force = 99.95f;

        #endregion


        #region Methods

        public override void Fire()
        {
            if (!_isRedy)
            {
                return;
            }

            var ammunition = Instantiate(_ammunitionPrefab, _barrel.position, _barrel.rotation);
            ammunition.AddForce(_barrel.forward * _force);
            _isRedy = false;
            _timeRemaining.AddTimeRemaining();
        }

        #endregion
    }
}
