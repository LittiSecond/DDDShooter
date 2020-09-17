using System;
using UnityEngine;
using Geekbrains;


namespace DddShooter
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
                //ServiceLocatorMonoBehaviour.GetService<PauseMenu>().Hide();
                ServiceLocatorMonoBehaviour.GetService<UiPanelManager>().Execute(UiPanelType.None);
            }
            else
            {
                PauseOn();
                //ServiceLocatorMonoBehaviour.GetService<PauseMenu>().Show();
                ServiceLocatorMonoBehaviour.GetService<UiPanelManager>().Execute(UiPanelType.MenuPause);
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
            Time.timeScale = 0;
            _pauseMessageUiText.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            _isPause = true;
        }

        private void PauseOff()
        {
            Time.timeScale = 1;
            _pauseMessageUiText.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            _isPause = false;
        }

        #endregion

    }
}
