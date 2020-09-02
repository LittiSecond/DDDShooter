using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class Gun : Weapon
    {
        #region Fields

        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private ShootFlash _shootFlash;
        [SerializeField] private float _force = 99.95f;
        [SerializeField] private bool _endlessAmmunition;     // must used only for enemy

        private bool _haveShootEffect = false;

        #endregion


        #region UnityMethods

        protected override void Start()
        {
            base.Start();
            _haveShootEffect = _shootFlash != null;
        }

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
                    if (_projectilePrefab)
                    {
                        var ammunition = CreateProjectile();
                        ammunition.AddForce(_barrel.forward * _force);
                    }
                    ActivateShootEffect();
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

        private Projectile CreateProjectile()
        {
                return Instantiate(_projectilePrefab, _barrel.position, _barrel.rotation);
        }

        public override void DisableEndlessAmmunition()
        {
            _endlessAmmunition = false;
        }

        private void ActivateShootEffect()
        {
            if (_haveShootEffect)
            {
                _shootFlash.Activate();
            }
        }

        #endregion
    }
}
