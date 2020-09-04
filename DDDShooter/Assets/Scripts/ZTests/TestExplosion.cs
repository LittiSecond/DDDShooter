using UnityEngine;


namespace DddShooter.Test
{
    public class TestExplosion : MonoBehaviour
    {
        public ExplosionEffect _explosionEffect;

        private KeyCode _key = KeyCode.L;

        public void RunTest()
        {
            if (_explosionEffect)
            {
                _explosionEffect.ActivateInDebug();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(_key))
            {
                RunTest();
            }
        }

    }
}