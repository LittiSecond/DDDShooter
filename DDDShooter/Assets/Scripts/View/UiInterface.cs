using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class UiInterface
    {
        private FlashLightUiText _flashLightUiText;
        private PauseMessageUiText _pauseMessageUiText;
        private MessageUiText _messageUiText;
        private UiClipInfo _uiClipInfo;

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
        
        public MessageUiText MessageText
        {
            get
            {
                if (!_messageUiText)
                {
                    _messageUiText = Object.FindObjectOfType<MessageUiText>();
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

    }
}