using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class UiInterface
    {
        private static FlashLightUiText _flashLightUiText;
        private static PauseMessageUiText _pauseMessageUiText;
        private static UiInteractMessageText _messageUiText;
        private static UiClipInfo _uiClipInfo;
        private static UiWarningMessageText _warningMessageText;

        public FlashLightUiText LightUiText
        {
            get
            {
                if (!_flashLightUiText)
                    _flashLightUiText = Object.FindObjectOfType<FlashLightUiText>();
                return _flashLightUiText;
            }
        }

        public PauseMessageUiText PauseUiText
        {
            get
            {
                if (!_pauseMessageUiText)
                {
                    _pauseMessageUiText = Object.FindObjectOfType<PauseMessageUiText>();
                }
                return _pauseMessageUiText;
            }
        }
        
        public UiInteractMessageText InteractMessageText
        {
            get
            {
                if (!_messageUiText)
                {
                    _messageUiText = Object.FindObjectOfType<UiInteractMessageText>();
                }
                return _messageUiText;
            }
        }

        public UiClipInfo UiClipInfoPanel
        {
            get
            {
                if (!_uiClipInfo)
                    _uiClipInfo = Object.FindObjectOfType<UiClipInfo>();
                return _uiClipInfo;
            }
        }

        public UiWarningMessageText WarningMessageText
        {
            get
            {
                if (!_warningMessageText)
                    _warningMessageText = Object.FindObjectOfType<UiWarningMessageText>();
                return _warningMessageText;
            }
        }


    }
}