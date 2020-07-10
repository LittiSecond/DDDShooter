using UnityEngine;
using DddShooter;


namespace Geekbrains
{
    public sealed class UiInterface
    {
        private FlashLightUiText _flashLightUiText;
        private PauseMessageUiText _pauseMessageUiText;      // <---added
        private MessageUiText _messageUiText;                // <---added

        public FlashLightUiText LightUiText
        {
            get
            {
                if (!_flashLightUiText)
                    _flashLightUiText = Object.FindObjectOfType<FlashLightUiText>();
                return _flashLightUiText;
            }
        }

        #region added
        //------------------------------------------------ added
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

        #endregion
    }
}