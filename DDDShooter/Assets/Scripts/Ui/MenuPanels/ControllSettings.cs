using UnityEngine;
using UnityEngine.UI;

using Geekbrains;


namespace DddShooter
{
    public sealed class ControllSettings : BaseMenu
    {
        #region Fields

        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private Text _panelCaption;
        [SerializeField] private Text _infoText;
        [SerializeField] private ButtonUi _close;
                
        #endregion


        #region UnityMethods

        private void Start()
        {
            _close.GetControl.onClick.AddListener(Close);

            IsShow = true;
            Hide();

            TranslateTexts();
        }

        #endregion


        #region Methods

        public override void Hide()
        {
            if (!IsShow) return;
            _mainPanel.gameObject.SetActive(false);
            IsShow = false;
        }

        public override void Show()
        {
            if (IsShow) return;
            _mainPanel.gameObject.SetActive(true);
            IsShow = true;
        }

        protected override void TranslateTexts()
        {
            _panelCaption.text = LangManager.Instance.Text(
                TextConstants.MENU_ITEMS_GROUP_ID, TextConstants.CONTROLL_OPTIONS_CAPTION_TEXT_ID);
            _infoText.text = LangManager.Instance.Text(
                TextConstants.BIG_TEXT_GROUP_ID, TextConstants.CONTROLL_DESCRIPTION_TEXT_ID);

            _close.GetText.text = LangManager.Instance.Text(
                TextConstants.MENU_ITEMS_GROUP_ID, TextConstants.CLOSE_TEXT_ID);
        }       

        private void Close()
        {
            UiPanelManager.ReturnToPrevious();
        }

        #endregion

    }
}
