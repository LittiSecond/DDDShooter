using System;

namespace DddShooter
{
    public sealed class EnemyHealth
    {
        #region Fields
        
        private float _heath = 200.0f;
        
        #endregion
        

        public event Action OnDeathEventHandler;
        

        #region ClassLifeCycles

        //public EnemyHealth(GameObject root)
        //{
        //    _damagTranslators = root.GetComponentsInChildren<EnemyPartDamagTranslator>();
        //    if (_damagTranslators != null)
        //    {
        //        foreach (var t in _damagTranslators)
        //        {
        //            t.OnDamagedEvent += TakeDamage;
        //        }
        //    }
        //}

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
                OnDeathEventHandler?.Invoke();
        }

        #endregion


    }
}
