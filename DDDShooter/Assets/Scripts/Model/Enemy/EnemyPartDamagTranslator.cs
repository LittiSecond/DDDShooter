using UnityEngine;
using System;


namespace DddShooter
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class EnemyPartDamagTranslator : MonoBehaviour, ITakerDamage, ITakerHealing
    {
        #region Fields

        public event Action<float> OnDamagedEvent;
        public event Action<float> OnHealingEvent;

        //private Rigidbody _rigidbody;

        private bool _isDead;

        #endregion


        #region Metods

        public void Die()
        {
            _isDead = true;
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }

        #endregion


        #region ITakerDamage

        public void TakeDamage(float damag)
        {
            if (!_isDead)
            {
                OnDamagedEvent?.Invoke(damag);
            }
        }

        #endregion

        #region ITakerHealing

        public void TakeHealing(float healing)
        {
            if (!_isDead)
            {
                OnHealingEvent?.Invoke(healing);
            }
        }

        #endregion
    }
}