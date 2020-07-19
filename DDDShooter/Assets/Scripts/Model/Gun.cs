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

        public override ShotResult Fire()
        {
            if (!_isRedy)
            {
                return ShotResult.NotReady;
            }

            if (_clip != null)
            {
                if (_clip.Extract())
                {
                    var ammunition = CreateAmmunition();
                    ammunition.AddForce(_barrel.forward * _force);
                    _isRedy = false;
                    _timeRemaining.AddTimeRemaining();
                }
            }

            if (_isRedy)
            {
                return ShotResult.NoAmmo;
            }
            return ShotResult.Done;
        }

 

        private Ammunition CreateAmmunition()
        {
            return Instantiate(_ammunitionPrefab, _barrel.position, _barrel.rotation);
        }

        #endregion
    }
}
