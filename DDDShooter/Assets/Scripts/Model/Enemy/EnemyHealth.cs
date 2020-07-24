using UnityEngine;
using Geekbrains;
using System;

namespace DddShooter
{
    public sealed class EnemyHealth : BaseObjectScene
    {
        #region Fields

        [SerializeField] private float _heath = 200.0f;

        private EnemyPartDamagTranslator[] _damagTranslators;

        public event Action OnDeathEvent;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _damagTranslators = GetComponentsInChildren<EnemyPartDamagTranslator>();
            if (_damagTranslators != null)
            {
                foreach (var t in _damagTranslators)
                {
                    t.OnDamagedEvent += TakeDamage;
                }
            }
        }

        #endregion


        #region Methods

        public void TakeDamage(float damag)
        {
            if (damag > 0 || _heath > 0)
            {
                _heath -= damag;
                if (_heath <= 0)
                {
                    DestroyItself();
                }
            }
        }

        private void DestroyItself()
        {
            if (_damagTranslators != null)
            {
                foreach (var t in _damagTranslators)
                {
                    t.Die();
                }
                OnDeathEvent?.Invoke();
            }
        }

        #endregion
    }
}
