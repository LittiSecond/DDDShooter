using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    public class ExplosionEffect : MonoBehaviour
    {

        #region Fields

        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private float _multiplier = 1;

        private Light _light;
        private AudioSource _audioSource;
        private ParticleSystem[] _systems;
        private TimeRemaining _timer;

        private bool _isActive = false;
        private bool _isParticleSystemReady = false;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _light = GetComponent<Light>();
            _light.enabled = false;
            _timer = new TimeRemaining(DestroyItself, _duration);

            _audioSource = GetComponent<AudioSource>();

            _systems = GetComponentsInChildren<ParticleSystem>();
            if (_systems != null)
                if (_systems.Length > 0)
                {
                    //foreach (ParticleSystem system in _systems)
                    for (int i = 0; i < _systems.Length; i++)
                    {
                        ParticleSystem.MainModule mainModule = _systems[i].main;
                        mainModule.startSizeMultiplier *= _multiplier;
                        mainModule.startSpeedMultiplier *= _multiplier;
                        mainModule.startLifetimeMultiplier *= Mathf.Lerp(_multiplier, 1, 0.5f);
                        _systems[i].Stop();
                        _isParticleSystemReady = true;
                    }
                }
        }

        #endregion


        #region Methods

        public void Activate()
        {
            if (!_isActive)
            {
                ActivateAll();
                _audioSource.Play();
                _light.enabled = true;
                _timer.AddTimeRemaining();
                _isActive = true;
            }
        }

        public void ActivateInDebug()
        {
            //if (!_isActive)
            {
                ActivateAll();
                //_light.enabled = true;
                //_timer.AddTimeRemaining();
               // _isActive = true;
            }
        }

        private void DestroyItself()
        {
            StopAll();
            _light.enabled = false;
            _isActive = false;
            Destroy(gameObject);
        }

        private void StopAll()
        {
            if (_isParticleSystemReady)
            {
                for (int i = 0; i < _systems.Length; i++)
                {
                    _systems[i].Stop();
                }
            }
        }

        private void ActivateAll()
        {
            if (_isParticleSystemReady)
            {
                for (int i = 0; i < _systems.Length; i++)
                {
                    _systems[i].Play();
                }
            }
        }

        #endregion
    }
}