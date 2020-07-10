using UnityEngine;
using UnityEngine.UI;
using Geekbrains;


namespace DddShooter
{
    public class UiClipInfo : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _caption;
        [SerializeField] private Text _clipEmptyMessage;
        [SerializeField] private Text _ammoInfo;

        [SerializeField] private float _delay = 4.0f;

        private ITimeRemaining _timeRemaining;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _timeRemaining = new TimeRemaining(HideClipEmptyMessage, _delay);
            Hide();
            HideClipEmptyMessage();
        }

        #endregion


        #region Methods

        public void Show()
        {
            _caption.enabled = true;
            _ammoInfo.enabled = true;
        }

        public void Hide()
        {
            _caption.enabled = false;
            _ammoInfo.enabled = false;
        }

        public void ShowData(int currentQuantity, int clipCapasity)
        {
            _ammoInfo.text = $"{currentQuantity} / {clipCapasity}";
        }

        public void ShowClipEmptyMessage()
        {
            _clipEmptyMessage.enabled = true;
            _timeRemaining.AddTimeRemaining();
        }

        public void HideClipEmptyMessage()
        {
            _clipEmptyMessage.enabled = false;
        }

        #endregion
    }
}