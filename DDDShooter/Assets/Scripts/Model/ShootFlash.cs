using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    [RequireComponent(typeof(Light)), RequireComponent(typeof(MeshRenderer))]
    public class ShootFlash : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _duration = 0.5f;

        private Light _light;
        private MeshRenderer _meshRenderer;
        private TimeRemaining _timer;

        private bool _isActive = false;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _light = GetComponent<Light>();
            _light.enabled = false;
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.enabled = false;
            _timer = new TimeRemaining(Deactivate, _duration);
        }

        #endregion


        #region Methods

        public void Activate()
        {
            if (!_isActive)
            {
                _light.enabled = true;
                _meshRenderer.enabled = true;
                _timer.AddTimeRemaining();
                _isActive = true;
            }
        }

        private void Deactivate()
        {
            if (_isActive)
            {
                _light.enabled = false;
                _meshRenderer.enabled = false;
                _isActive = false;
            }
        }

        #endregion


    }
}