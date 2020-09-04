using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public abstract class Projectile : BaseObjectScene
    {
        #region Fields

        //[SerializeField] protected ExplosionEffectPrototype _explosionPrefab;
        [SerializeField] protected ExplosionEffect _explosionPrefab;
        [SerializeField] protected float _timeToDestruct = 12.0f;
        [SerializeField] protected float _baseDamage = 10.0f;
        
        protected float _startTime;
        private ITimeRemaining _timeRemaining;

        private bool _haveDestractionEffect = false;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _timeRemaining = new TimeRemaining(DestroyItself, _timeToDestruct);
            _timeRemaining.AddTimeRemaining();
            _startTime = Time.time;
            _haveDestractionEffect = _explosionPrefab != null;
        }

        #endregion


        #region Methods
        
        public void AddForce(Vector3 dir)
        {
            if (Rigidbody)
            {
                Rigidbody.AddForce(dir, ForceMode.VelocityChange);
            }
        }

        protected virtual void DestroyItself()
        {
            _timeRemaining.RemoveTimeRemaining();
            CreateDestractionEffect();
            Destroy(gameObject);
        }

        protected virtual void CreateDestractionEffect()
        {
            if (_haveDestractionEffect)
            {
                //ExplosionEffectPrototype effect = Instantiate<ExplosionEffectPrototype>(_explosionPrefab, Transform.position, Transform.rotation );
                ExplosionEffect effect = Instantiate<ExplosionEffect>(_explosionPrefab, Transform.position, Transform.rotation );
                effect.Activate();
            }
        }

        #endregion
    }
}
