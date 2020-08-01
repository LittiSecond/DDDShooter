using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class StrangeObjectScene : BaseObjectScene
    {
        [SerializeField] private Transform _oneTransform;
        [SerializeField] private int _value;




        private void DoNull()
        {
            _oneTransform = null;
            _value = 0;
        }

    }
}
