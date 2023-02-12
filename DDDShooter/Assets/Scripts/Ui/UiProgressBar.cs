using UnityEngine;

using Geekbrains;

namespace DddShooter
{
    // Interface
    public sealed class UiProgressBar : SliderUI
    {

        [SerializeField] private GameObject _sliderGameObject;


        private void Start()
        {
            if (!_sliderGameObject)
            {
                _sliderGameObject = gameObject;
            }
        }


        public void SetActive(bool value)
        {
            if (_sliderGameObject)
            {
                _sliderGameObject.SetActive(value);
            }
        }

    }
}
