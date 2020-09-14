using UnityEngine;


namespace DddShooter
{
    public sealed class Bullet : Projectile
    {
        #region Fields

        [SerializeField] private float _damagLosingPerSecond = 1.0f;

        #endregion


        #region UnityMethods

        private void OnCollisionEnter(Collision collision)
        {
            ITakerDamage takerDamage = collision.gameObject.GetComponent<ITakerDamage>();
            if (takerDamage != null)
            {
                takerDamage.TakeDamage(RecalculateDamag());
            }

            DestroyItself();
        }

        #endregion


        #region Methods

        private float RecalculateDamag()
        {
            float currentTime = Time.time;
            float lifeDuration = currentTime - _startTime;
            float damag = _baseDamage - _damagLosingPerSecond * lifeDuration;
            if (damag < 0)
            {
                damag = 0;
            }
            return damag;
        }

        #endregion
    }
}
