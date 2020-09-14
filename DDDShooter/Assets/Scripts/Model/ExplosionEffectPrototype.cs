using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    [RequireComponent(typeof(Light)), RequireComponent(typeof(MeshRenderer))]
    public class ExplosionEffectPrototype : MonoBehaviour
    {

        #region Fields

        [SerializeField] private float _duration = 0.5f;

        private Light _light;
        private MeshRenderer _meshRenderer;
        private TimeRemaining _timer;

        private bool _isActive = false;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _light = GetComponent<Light>();
            _light.enabled = false;
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.enabled = false;
            _timer = new TimeRemaining(DestroyItself, _duration);
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

        private void DestroyItself()
        {

                _light.enabled = false;
                _meshRenderer.enabled = false;
                _isActive = false;
            Destroy(gameObject);
            
        }

        #endregion
    }
}