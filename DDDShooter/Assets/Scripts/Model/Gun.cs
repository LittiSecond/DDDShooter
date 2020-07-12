using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class Gun : Weapon
    {
        #region Fields

        [SerializeField] private Ammunition _ammunitionPrefab;
        [SerializeField] private float _force = 99.95f;

        private const int CLIP_IS_EMTY_MESSAGE_ID = 4;

        #endregion


        #region Methods

        public override void Fire()
        {
            if (!_isRedy)
            {
                return;
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
                _warningMessageText.Show(CLIP_IS_EMTY_MESSAGE_ID);
            }

        }

 

        private Ammunition CreateAmmunition()
        {
            return Instantiate(_ammunitionPrefab, _barrel.position, _barrel.rotation);
        }

        #endregion
    }
}
