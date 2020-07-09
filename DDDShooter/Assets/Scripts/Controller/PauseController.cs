using System;
using UnityEngine;

namespace Geekbrains
{
    public sealed class PauseController : BaseController
    {
        #region Fields

        private PauseMessageUiText _pauseMessageUiText;
        private bool _isPause = false;

        public event Action<bool> SwichPauseEvent;

        #endregion


        #region Methods

        public void SwithPause()
        {
            //CustumDebug.Log($"PauseController->SwithPause: IsActive = {IsActive} + _isPause = {_isPause}");
            if (!IsActive) return;
            if (_isPause)
            {
                PauseOff();
            }
            else
            {
                PauseOn();
            }

            SwichPauseEvent?.Invoke(_isPause);
        }

        public override void On()
        {
            base.On();
            _pauseMessageUiText = UiInterface.PauseUiText;
            _pauseMessageUiText.SetActive(false);
        }

        private void PauseOn()
        {
            ServiceLocator.Resolve<PlayerController>().Off();
            Time.timeScale = 0;
            _pauseMessageUiText.SetActive(true);
            _isPause = true;
        }

        private void PauseOff()
        {
            ServiceLocator.Resolve<PlayerController>().On();
            Time.timeScale = 1;
            _pauseMessageUiText.SetActive(false);
            _isPause = false;
        }

        #endregion

    }
}
