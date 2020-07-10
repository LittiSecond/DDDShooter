using UnityEngine;


namespace Geekbrains
{
    public abstract class Weapon : BaseObjectScene
    {
        #region Fields

        [SerializeField] protected Transform _barrel;

        [SerializeField] protected float _rechargeTime = 1.0f;

        protected ITimeRemaining _timeRemaining;
        protected bool _isRedy = true;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _timeRemaining = new TimeRemaining(ReadyShoot, _rechargeTime);
        }

        #endregion


        #region Methods

        public abstract void Fire();

        protected void ReadyShoot()
        {
            _isRedy = true;
        }

        #endregion
    }
}
