using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

using Geekbrains;


namespace DddShooter
{
    public class SoundOptionsPanel : BaseMenu
    {
        #region Fields

        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private Text _panelCaption;
        [SerializeField] private Text _generalText;
        [SerializeField] private Text _bgMusicText;
        [SerializeField] private Text _soundsText;
        [SerializeField] private SliderUI _generalSlider;
        [SerializeField] private SliderUI _bgMusicSlider;
        [SerializeField] private SliderUI _soundsSlider;
        [SerializeField] private ButtonUi _close;

        private AudioMixer _audioMixer;

        //private 


        private const string AUDIO_MIXER_GENERAL_VOLUME = "GeneralVolume";
        private const string AUDIO_MIXER_BG_MUSIC_VOLUME = "BGMusicVolume";
        private const string AUDIO_MIXER_SOUNDS_VOLUME = "SoundsVolume";

        #endregion


        #region UnityMethods
        private void Start()
        {
            _audioMixer = ResourcesManager.GetAudioMixer();

            _generalSlider.GetControl.onValueChanged.AddListener(ChangeGeneralVolume);
            _bgMusicSlider.GetControl.onValueChanged.AddListener(ChangeMusicVolume);
            _soundsSlider.GetControl.onValueChanged.AddListener(ChangeSoundVolume);
            _close.GetControl.onClick.AddListener(Close);

            IsShow = true;
            Hide();

            LoadSettings();
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
            LoadSettings();
            IsShow = true;
        }

        private void TranslateTexts()
        {
            _panelCaption.text = LangManager.Instance.Text(
                TextConstants.MENU_ITEMS_GROUP_ID, TextConstants.SOUND_OPTIONS_CAPTION_TEXT_ID);
            _generalText.text = LangManager.Instance.Text(
                TextConstants.MENU_ITEMS_GROUP_ID, TextConstants.GENERAL_VOLUME_TEXT_ID);
            _bgMusicText.text = LangManager.Instance.Text(
                TextConstants.MENU_ITEMS_GROUP_ID, TextConstants.BG_MUSIC_VOLUME_TEXT_ID);
            _soundsText.text = LangManager.Instance.Text(
                TextConstants.MENU_ITEMS_GROUP_ID, TextConstants.SOUNDS_VOLUME_TEXT_ID);
        }

        private void LoadSettings()
        {
            float general;
            float bgMusic;
            float sounds;

            _audioMixer.GetFloat(AUDIO_MIXER_GENERAL_VOLUME, out general);
            _audioMixer.GetFloat(AUDIO_MIXER_BG_MUSIC_VOLUME, out bgMusic);
            _audioMixer.GetFloat(AUDIO_MIXER_SOUNDS_VOLUME, out sounds);

            CustumDebug.Log($"SoundOptionsPanel->LoadSettings: {general}, {bgMusic}, {sounds} ");

            //                  TODO: add recalculation sound volume diapasons [-80, 20] -> [0, 1]
            _generalSlider.GetControl.value = general;
            _bgMusicSlider.GetControl.value = bgMusic;
            _soundsSlider.GetControl.value = sounds;
        }

        private void ChangeGeneralVolume(float newVolume)
        {
            //                  TODO: add recalculation sound volume diapasons [0, 1] -> [-80, 20]
            _audioMixer.SetFloat(AUDIO_MIXER_GENERAL_VOLUME, newVolume);
        }

        private void ChangeMusicVolume(float newVolume)
        {
            _audioMixer.SetFloat(AUDIO_MIXER_BG_MUSIC_VOLUME, newVolume);
        }

        private void ChangeSoundVolume(float newVolume)
        {
            _audioMixer.SetFloat(AUDIO_MIXER_SOUNDS_VOLUME, newVolume);
        }

        private void Close()
        {
            UiPanelManager.Execute(UiPanelType.OptionsMenu);
        }

        #endregion
    }
}
