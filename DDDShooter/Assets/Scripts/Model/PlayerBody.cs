using System;
using System.Collections.Generic;
using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    public sealed class PlayerBody : BaseObjectScene, ITakerDamage, ITakerHealing
    {
        #region Fields

        [SerializeField] private Transform _bodyCentre;
        [SerializeField] private Transform _head;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private CharacterController _characterController;

        [SerializeField] private PlayerSoundContainer _soundContainer;

        //[SerializeField] private Vector3 _cameraOffset = new Vector3(0.0f, 0.0f, 0.0f);

        private Transform _cameraTransform;

        public event Action<float> OnDamagedEvent = delegate { };
        public event Action<float> OnHealingEvent = delegate { };

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            if (!_bodyCentre)
            {
                _bodyCentre = Transform;
            }
            if (!_head)
            {
                _head = Transform;
            }
            if (!_audioSource)
            {
                _audioSource = Transform.GetComponent<AudioSource>();
            }
            if (!_characterController)
            {
                _characterController = Transform.GetComponent<CharacterController>();
            }
        }

        #endregion


        #region Properties

        public Transform BodyCentre { get => _bodyCentre; }

        public Transform Head { get => _head; }

        public PlayerSoundContainer SoundContainer { get => _soundContainer; }

        public AudioSource AudioSource { get => _audioSource; }

        public CharacterController CharacterController { get => _characterController; }

        #endregion


        #region Methods

        //public void FixCamera(Transform cameraTransform)
        //{
        //    _cameraTransform = cameraTransform;
        //    _cameraTransform.SetParent(_head);
        //    _cameraTransform.localPosition = _cameraOffset;
        //}

        public void DropCamera()
        {
            if (_cameraTransform)
            {
                _cameraTransform.SetParent(null);
                _cameraTransform = null;
            }
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        #endregion


        #region ITakerDamage

        public void TakeDamage(float damag)
        {
            OnDamagedEvent.Invoke(damag);
        }

        #endregion


        #region ITakerHealing

        public void TakeHealing(float healing)
        {
            OnHealingEvent.Invoke(healing);
        }

        #endregion

    }
}
