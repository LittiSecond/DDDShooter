using UnityEngine;
using UnityEngine.UI;
using Geekbrains;


namespace DddShooter
{
    public sealed class UiWarningMessageText : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _warningMessage;

        [SerializeField] private float _delay = 4.0f;

        private ITimeRemaining _timeRemaining;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _timeRemaining = new TimeRemaining(Hide, _delay);
            Hide();
        }

        #endregion


        #region Methods

        public void Show(int textId)
        {
            _warningMessage.text = TextConstants.GetText(textId);
            _warningMessage.enabled = true;
            _timeRemaining.AddTimeRemaining();
        }

        public void Hide()
        {
            _warningMessage.enabled = false;
        }

        #endregion
    }
}