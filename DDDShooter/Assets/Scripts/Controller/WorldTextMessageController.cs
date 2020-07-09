using UnityEngine;

namespace Geekbrains
{
    public sealed class WorldTextMessageController : MonoBehaviour
    {
        #region Fields

        private TextMesh _textMesh;
        private Renderer _renderer;

        ITimeRemaining _timeRemaining;

        private float _defoultBackCountingInterval = 1.0f;
        private int _defoultBackCountingStartValue = 5;
        private int _counter;

        private bool _isBackCounting;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _textMesh = GetComponent<TextMesh>();
            _renderer = GetComponent<Renderer>();
            if (_renderer)
            {
                _renderer.enabled = false;
            }
        }

        #endregion


        #region Methods

        public void StartBackCounting(int startValue)
        {
            //CustumDebug.Log("WorldTextMessageController->StartBackCounting: startValue = " + startValue.ToString());
            if (_isBackCounting)
            {
                StopBackCounting();
            }

            _counter = startValue;
            if (_renderer && _textMesh)
            {
                _textMesh.text = _counter.ToString();
                _renderer.enabled = true;

                _timeRemaining = new TimeRemaining(Count, _defoultBackCountingInterval, true);
                _timeRemaining.AddTimeRemaining();
                _isBackCounting = true;
            }
        }

        public void StartBackCounting()
        {
            StartBackCounting(_defoultBackCountingStartValue);
        }

        private void Count()
        {
            if (_counter <= 0)
            {
                StopBackCounting();
            }
            else
            {
                _counter -= 1;
                if (_textMesh)
                {
                    _textMesh.text = _counter.ToString();
                }
            }
        }

        private void StopBackCounting()
        {
            if (_renderer)
            {
                _renderer.enabled = false;
            }
            _timeRemaining.RemoveTimeRemaining();
            _isBackCounting = false;
        }

        #endregion
    }
}