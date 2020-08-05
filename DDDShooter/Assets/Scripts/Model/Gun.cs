using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class Gun : Weapon
    {
        #region Fields

        [SerializeField] private Ammunition _ammunitionPrefab;
        [SerializeField] private float _force = 99.95f;
        [SerializeField] private bool _endlessAmmunition;     // must used only for enemy

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
                if (_endlessAmmunition || _clip.Extract())
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

        public override void DisableEndlessAmmunition()
        {
            _endlessAmmunition = false;
        }

        #endregion
    }
}
