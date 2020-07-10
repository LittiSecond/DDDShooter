using UnityEngine;


namespace Geekbrains
{
    public abstract class Ammunition : BaseObjectScene
    {
        #region Fields

        [SerializeField] protected float _timeToDestruct = 12.0f;
        [SerializeField] protected float _baseDamage = 10.0f;

        private ITimeRemaining _timeRemaining;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _timeRemaining = new TimeRemaining(DestroyItself, _timeToDestruct);
            _timeRemaining.AddTimeRemaining();
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
            Destroy(gameObject);
        }

        #endregion
    }
}
