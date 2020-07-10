using UnityEngine;
using System;


namespace DddShooter
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class EnemyPartDamagTranslator : MonoBehaviour, ITakerDamage
    {
        #region Fields

        public event Action<float> OnDamagedEvent;

        //private Rigidbody _rigidbody;

        private bool _isDead;
        
        #endregion


        #region ITakerDamage

        public void TakeDamage(float damag)
        {
            if (!_isDead)
            {
                OnDamagedEvent?.Invoke(damag);
            }
        }

        public void Die()
        {
            _isDead = true;
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }

        #endregion
    }
}