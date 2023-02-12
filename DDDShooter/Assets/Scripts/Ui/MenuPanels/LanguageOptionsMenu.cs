using UnityEngine;
using UnityEngine.UI;

using Geekbrains;


namespace DddShooter
{
    public sealed class LanguageOptionsMenu : BaseMenu
    {
        #region Fields

        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private Text _caption;
        [SerializeField] private ButtonUi _english;
        [SerializeField] private ButtonUi _russian;
        [SerializeField] private ButtonUi _back;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _english.GetControl.onClick.AddListener(SetEnglish);
            _russian.GetControl.onClick.AddListener(SetRussian);
            _back.GetControl.onClick.AddListener(Back);

            IsShow = true;
            Hide();

            TranslateTexts();
        }

        #endregion


        #region Methods

        private void SetEnglish()
        {            
            LangManager.Instance.SwitchLanguage(TextConstants.LANGUAGE_CODE_EN);
        }

        private void SetRussian()
        {
            LangManager.Instance.SwitchLanguage(TextConstants.LANGUAGE_CODE_RU);
        }

        private void Back()
        {
            UiPanelManager.ReturnToPrevious();
        }

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
            _caption.text = LangManager.Instance.Text(
                TextConstants.MENU_ITEMS_GROUP_ID, TextConstants.LANGUAGE_TEXT_ID);
            //_english.GetText.text = TextConstants.ENGLISH_TEXT;
            //_russian.GetText.text = TextConstants.RUSSIAN_TEXT;
            _back.GetText.text = LangManager.Instance.Text(
                TextConstants.MENU_ITEMS_GROUP_ID, TextConstants.CLOSE_TEXT_ID);
        }

        #endregion
    }
}
