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

        //[SerializeField] private Vector3 _cameraOffset = new Vector3(0.0f, 0.0f, 0.0f);

        private Transform _cameraTransform;

        public event Action<float> OnDamagedEvent = delegate { };
        public event Action<float> OnHealingEvent = delegate { };

        #endregion


        #region Properties

        public Transform BodyCentre { get => _bodyCentre; }

        public Transform Head { get => _head; }

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
