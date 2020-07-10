using UnityEngine;

namespace Geekbrains
{
    public sealed class Bullet : Ammunition
    {
        #region UnityMethods

        private void OnCollisionEnter(Collision collision)
        {
            ITakerDamage takerDamage = collision.gameObject.GetComponent<ITakerDamage>();
            if (takerDamage != null)
            {
                takerDamage.TakeDamage(_baseDamage);
            }

            DestroyItself();
        }

        #endregion
    }
}
