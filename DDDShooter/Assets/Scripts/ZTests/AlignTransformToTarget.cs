using UnityEngine;


namespace DddShooter.Test
{
    public class AlignTransformToTarget : MonoBehaviour
    {
        [SerializeField] private Transform _objectForAlign;
        [SerializeField] private Transform _target;

        private KeyCode _key = KeyCode.L;

        public void Align()
        {
            if (_objectForAlign && _target)
            {
                _objectForAlign.LookAt(_target);
            }

        }

        private void Update()
        {
            if (Input.GetKeyDown(_key))
            {
                Align();
            }
        }

    }
}