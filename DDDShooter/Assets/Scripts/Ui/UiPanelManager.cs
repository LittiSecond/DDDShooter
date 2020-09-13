using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
        //private AudioOptions _audioOptions;
        //private MenuPause _menuPause;
        //private OptionsPauseMenu _optionsPauseMenu;
        #endregion
        private void Start()
        {
            InterfaceResources = GetComponent<InterfaceResources>();
            _mainMenu = GetComponent<MainMenu>();
            _optionsMenu = GetComponent<OptionsMenu>();
            //_videoOptions = GetComponent<VideoOptions>();
            //_gameOptions = GetComponent<GameOptions>();
            //_audioOptions = GetComponent<AudioOptions>();
            //_menuPause = GetComponent<MenuPause>();
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
                //case UiPanelType.AudioOptions:
                // if (_currentMenu != null) _currentMenu.Hide();
                // _currentMenu = _audioOptions;
                // _currentMenu.Show();
                // break;
                //case UiPanelType.GameOptions:
                // if (_currentMenu != null) _currentMenu.Hide();
                // _currentMenu = _gameOptions;
                // _currentMenu.Show();
                // break;
                //case UiPanelType.MenuPause:
                // if (_currentMenu != null) _currentMenu.Hide();
                // _currentMenu = _menuPause;
                // _currentMenu.Show();
                // break;
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //
            }
        }

        #region ProgressBar
        public void ProgressBarSetValue(float value)
        {
            if (_progressBar == null) return;
            _progressBar.GetControl.value = value;
            _progressBar.GetText.text = $"{Math.Truncate(value * 100)}%";
        }
        public void ProgressBarEnabled()
        {
            if (_progressBar) return;
            _progressBar = Instantiate(InterfaceResources.ProgressbarPrefab, InterfaceResources.MainCanvas.transform);
            ProgressBarSetValue(0);
        }
        public void ProgressBarDisable()
        {
            if (!_progressBar) return;
            Destroy(_progressBar.Instance);
            //_progressBar = null;
        }
        #endregion

        #region LoadScene
        public void LoadSceneAsync(int lvl)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(lvl);
            StartCoroutine(LoadSceneAsync(async));
        }
        public void LoadSceneAsync(Scene lvl)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(lvl.buildIndex);
            StartCoroutine(LoadSceneAsync(async));
        }
        public void LoadSceneAsync(string lvl)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(lvl);
            StartCoroutine(LoadSceneAsync(async));
        }

        private IEnumerator LoadSceneAsync(AsyncOperation async)
        {
            ProgressBarEnabled();// todo
            async.allowSceneActivation = false;
            while (!async.isDone)
            {
                ProgressBarSetValue(async.progress + 0.1f);// todo
                float progress = async.progress * 100f;
                if (async.progress < 0.9f && Mathf.RoundToInt(progress) != 100)
                {
                    async.allowSceneActivation = false;
                }
                else
                {
                    if (async.allowSceneActivation) yield return null;
                    async.allowSceneActivation = true;
                    ProgressBarDisable();// todo
                }
                yield return null;
            }
        }
        #endregion
    }
}
