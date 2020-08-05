using UnityEngine;
using Geekbrains;


namespace DddShooter.Test
{
    public sealed class StrangeObjectScene : BaseObjectScene
    {
        //[SerializeField] private Transform _oneTransform;
        [SerializeField] private int _value; // не должен быть warning 0649

        private int _warning0649Provocator;  // должен быть warning 0649

        private void DoNull()
        {
            //_oneTransform = null;
            //_value = 0;
            int meaningless = _value + _warning0649Provocator;
        }

    }
}
