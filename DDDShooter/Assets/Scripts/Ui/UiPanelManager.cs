using UnityEngine;
using UnityEngine.EventSystems;

using DddShooter;


namespace Geekbrains
{
    // Interface
    public sealed class UiPanelManager : MonoBehaviour
    {
        public InterfaceResources InterfaceResources { get; private set; }

        private SliderUI _progressBar;
        private BaseMenu _currentMenu;

        //private readonly Stack<UiPanelType> _panelsStack = new Stack<UiPanelType>(); // TODO

        #region Object
        private MainMenu _mainMenu;
        private OptionsMenu _optionsMenu;
        //private VideoOptions _videoOptions;
        //private GameOptions _gameOptions;
        private SoundOptionsPanel _audioOptions;
        private PauseMenu _pauseMenu;
        //private OptionsPauseMenu _optionsPauseMenu;
        #endregion
        private void Start()
        {
            InterfaceResources = GetComponent<InterfaceResources>();
            _mainMenu = GetComponent<MainMenu>();
            _optionsMenu = GetComponent<OptionsMenu>();
            //_videoOptions = GetComponent<VideoOptions>();
            //_gameOptions = GetComponent<GameOptions>();
            _audioOptions = GetComponent<SoundOptionsPanel>();
            _pauseMenu = GetComponent<PauseMenu>();
            //_optionsPauseMenu = GetComponent<OptionsPauseMenu>();


            if (_mainMenu)
            {
                Execute(UiPanelType.MainMenu);
            }
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit ();
#endif
        }

        public void Execute(UiPanelType menuItem) // добавить отмену
        {
            if (_currentMenu != null) _currentMenu.Hide();
            switch (menuItem)
            {
                case UiPanelType.MainMenu:
                    _currentMenu = _mainMenu;
                    break;
                case UiPanelType.OptionsMenu:
                    _currentMenu = _optionsMenu;
                    break;
                //case UiPanelType.VideoOptions:
                // if (_currentMenu != null) _currentMenu.Hide();
                // _currentMenu = _videoOptions;
                // _currentMenu.Show();
                // break;
                case UiPanelType.AudioOptions:
                    if (_currentMenu != null) _currentMenu.Hide();
                    _currentMenu = _audioOptions;
                    _currentMenu.Show();
                    break;
                //case UiPanelType.GameOptions:
                // if (_currentMenu != null) _currentMenu.Hide();
                // _currentMenu = _gameOptions;
                // _currentMenu.Show();
                // break;
                case UiPanelType.MenuPause:
                    if (_currentMenu != null) _currentMenu.Hide();
                    _currentMenu = _pauseMenu;
                    _currentMenu.Show();
                    break;
                //case UiPanelType.OptionsPauseMenu:
                // if (_currentMenu != null) _currentMenu.Hide();
                // _currentMenu = _optionsPauseMenu;
                // _currentMenu.Show();
                // break;
                default:
                    break;
            }

            if (_currentMenu != null)
            {
                _currentMenu.Show();
                //_panelsStack.Push(menuItem);
            }
        }

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        Debug.Log("UiPanelManager::Update: Escape pressed.");
        //    }
        //}

        //#region ProgressBar
        //public void ProgressBarSetValue(float value)
        //{
        //    if (_progressBar == null) return;
        //    _progressBar.GetControl.value = value;
        //    _progressBar.GetText.text = $"{Math.Truncate(value * 100)}%";
        //}
        //public void ProgressBarEnabled()
        //{
        //    if (_progressBar) return;
        //    _progressBar = Instantiate(InterfaceResources.ProgressbarPrefab, InterfaceResources.MainCanvas.transform);
        //    ProgressBarSetValue(0);
        //}
        //public void ProgressBarDisable()
        //{
        //    if (!_progressBar) return;
        //    Destroy(_progressBar.Instance);
        //    //_progressBar = null;
        //}
        //#endregion
        
    }
}
