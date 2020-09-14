using UnityEngine;
using DddShooter;


namespace Geekbrains
{
    public abstract class Weapon : BaseObjectScene, IInteractable, IPickUpTool
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
        [Header("смещение от точки крепления к игроку")]
        [SerializeField] protected Vector3 _offSet = Vector3.zero;
        [SerializeField] protected Vector3 _rotationOffSet = Vector3.zero;

        protected UiClipInfo _uiClipInfo;
        protected ITimeRemaining _timeRemaining;
        private Collider _collider;
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

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<Collider>();
        }

        protected virtual void Start()
        {
            _timeRemaining = new TimeRemaining(ReadyShoot, _rechargeTime);
            _uiClipInfo = ServiceLocatorMonoBehaviour.GetService<UiClipInfo>();
        }

        #endregion


        #region Methods

        public abstract ShotResult Fire();

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

        public void JoinTo(Transform transformToJoin)
        {
            if (transformToJoin)
            {
                Transform.SetParent(transformToJoin);
                Transform.localPosition = _offSet;
                Transform.localRotation = Quaternion.identity;
                Transform.Rotate(_rotationOffSet);
            }
            else
            {
                Transform.SetParent(null);
            }
        }

        #endregion

        // реализацию этих интерфейсов и JoinTo()  надо убрать в другой класс...
        #region IInteractable    

        public bool IsTarget { set { } } // тут надо включать/выключать подсветку модели оружия, но 
                                         // эта подсветка пока не сделана

        public InteractType InteractType => InteractType.PickUpTool;

        public void Interact()
        {
            // nothing to do
        }

        public string GetMessageIfTarget()
        {
            return TextConstants.PICK_UP_TEXT_ID;
        }

        public virtual void DisableEndlessAmmunition()
        {

        }

        #endregion


        #region IPickUpTool

        public void DisablePhysics()
        {
            Rigidbody.isKinematic = true;
            Rigidbody.useGravity = false;
            _collider.enabled = false;
        }

        public void EnablePhysics()
        {
            Rigidbody.isKinematic = false;
            Rigidbody.useGravity = true;
            _collider.enabled = true;
        }

        #endregion
    }
}
