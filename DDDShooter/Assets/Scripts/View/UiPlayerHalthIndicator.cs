using UnityEngine;
using UnityEngine.UI;


namespace DddShooter
{
    public sealed class UiPlayerHalthIndicator : MonoBehaviour
    {
        #region Fields

        private Slider _slider;
        private Text _text;
        private float _max;
        private float _current;
        
        #endregion

        #region UnityMethods

        private void Awake()
        {
             _slider = gameObject.GetComponent<Slider>();
             _text = gameObject.GetComponentInChildren<Text>();
        }

        #endregion


        #region Methods

        public void SetValue(float current)
        {
            SetValue(current, _max);
        }

        public void SetValue(float current, float max)
        {
            if (_slider)
            {
                if (_max != max)
                {
                    _max = max;
                    _slider.maxValue = max;
                }
                if (_current != current)
                {
                    _current = current;
                    _slider.value = current;
                }
            }
            if (_text)
            {
                int cur = (int)_current;
                if (_current > 0 && _current < 1) cur = 1;
                _text.text = cur.ToString() + "/" + ((int)_max).ToString();
            }
        }

        #endregion
    }
}
