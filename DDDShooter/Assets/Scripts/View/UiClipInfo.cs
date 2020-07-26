using UnityEngine;
using UnityEngine.UI;


namespace DddShooter
{
    public class UiClipInfo : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _caption;
        [SerializeField] private Text _ammoInfo;
        
        #endregion


        #region UnityMethods

        private void Awake()
        {
            Hide();
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

        #endregion
    }
}