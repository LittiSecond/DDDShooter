using UnityEngine;
using UnityEngine.UI;

using Geekbrains;


namespace DddShooter
{
    public class OptionsMenu : BaseMenu
    {
        #region Fields

        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private Text _caption;
        [SerializeField] private ButtonUi _soundOptions;
        [SerializeField] private ButtonUi _languageOptions;
        [SerializeField] private ButtonUi _close;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _soundOptions.GetControl.onClick.AddListener(LoadSoundOptions);
            _languageOptions.GetControl.onClick.AddListener(LoadLanguageOptions);
            //_languageOptions.SetInteractable(false);
            _close.GetControl.onClick.AddListener(Back);
            
            IsShow = true;
            Hide();

            TranslateTexts();
        }

        #endregion


        #region Methods

        private void LoadSoundOptions()
        {
            UiPanelManager.Execute(UiPanelType.AudioOptions);
        }

        private void LoadLanguageOptions()
        {
            UiPanelManager.Execute(UiPanelType.LanguageOptions);
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
                TextConstants.MENU_ITEMS_GROUP_ID, TextConstants.OPTIONS_TEXT_ID);
            _soundOptions.GetText.text = LangManager.Instance.Text(
                TextConstants.MENU_ITEMS_GROUP_ID, TextConstants.SOUND_OPTIONS_TEXT_ID);
            _languageOptions.GetText.text = LangManager.Instance.Text(
                TextConstants.MENU_ITEMS_GROUP_ID, TextConstants.LANGUAGE_TEXT_ID);
            _close.GetText.text = LangManager.Instance.Text(
                TextConstants.MENU_ITEMS_GROUP_ID, TextConstants.CLOSE_TEXT_ID);
        }

        #endregion
    }
}