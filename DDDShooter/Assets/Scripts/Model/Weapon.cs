using UnityEngine;
using DddShooter;

namespace Geekbrains
{
    public abstract class Weapon : BaseObjectScene
    {
        #region Fields

        [SerializeField] protected Transform _barrel;
        [SerializeField] protected AmmunitionType _type;
        [SerializeField] protected float _rechargeTime = 1.0f;

        [SerializeField] protected Clip _clip;   // плохо спроектировал, не хочу давать этому классу
                                // Clip, так как энергетическому оружию
                                // Clip будет не нужен, ему будет нужна батарейка,
                                // а огнемёту вместо Clip будет нужена канистра.
                                //      потом подумаю, как перепроектировать.

        protected UiClipInfo _uiClipInfo;
        protected UiWarningMessageText _warningMessageText;
        protected ITimeRemaining _timeRemaining;
        protected bool _isRedy = true;

        #endregion


        #region Properties

        public virtual AmmunitionType Type
        {
            get => _type;
        }

        public Clip Clip
        {
            get => _clip;
        }

        //const Clip* Weapon::getClip() const
        //{
        //    return _clip;
        //}

        #endregion


        #region UnityMethods

        private void Start()
        {
            _timeRemaining = new TimeRemaining(ReadyShoot, _rechargeTime);
            _uiClipInfo = ServiceLocatorMonoBehaviour.GetService<UiClipInfo>();
            _warningMessageText = ServiceLocatorMonoBehaviour.GetService<UiWarningMessageText>();
        }

        #endregion


        #region Methods

        public abstract void Fire();

        protected void ReadyShoot()
        {
            _isRedy = true;
        }

        /// <summary>
        /// Peplace clip. 
        /// </summary>
        /// <param name=""></param>
        /// <returns> previous clip </returns>
        public virtual Clip ReloadClip(Clip newClip)
        {
            if (newClip != null)
            {
                if (newClip.Type != _type)
                {
                    // не знаю, что делать, если попытались зарядить обойму неправильного 
                    // типа. Не хочу кидать исключение.
                    CustumDebug.Log("Gun->ReloadClip: Error clip type.");
                    return newClip;
                }
            }

            Clip oldClip = _clip;
            _clip = newClip;
            return oldClip;
        }

        #endregion
    }
}
