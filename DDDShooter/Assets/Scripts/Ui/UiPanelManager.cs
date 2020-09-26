using System.Collections.Generic;
using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    // Interface
    public sealed class UiPanelManager : MonoBehaviour
    {
        #region Fields

        private MainMenu _mainMenu;
        private OptionsMenu _optionsMenu;
        private SoundOptionsPanel _audioOptions;
        private PauseMenu _pauseMenu;

        private BaseMenu _currentMenu;

        private readonly Stack<UiPanelType> _panelsStack = new Stack<UiPanelType>(); 

        #endregion


        #region UnityMethods

        private void Start()
        {
            _mainMenu = GetComponent<MainMenu>();
            _optionsMenu = GetComponent<OptionsMenu>();
            _audioOptions = GetComponent<SoundOptionsPanel>();
            _pauseMenu = GetComponent<PauseMenu>();

            if (_mainMenu)
            {
                Execute(UiPanelType.MainMenu);
            }
        }

        #endregion


        #region Methods

        public void QuitGame()  // TODO  выкинуть это отсюда, например в GameStarter
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit ();
#endif
        }

        public void Execute(UiPanelType menuItem)
        {
            if (_currentMenu != null) _currentMenu.Hide();

            if (menuItem == UiPanelType.None)
            {
                _panelsStack.Clear();
                _currentMenu = null;
            }
            else
            {
                _currentMenu = SelectMenu(menuItem);
                if (_currentMenu != null)
                {
                    _currentMenu.Show();
                    _panelsStack.Push(menuItem);
                }
            }
        }

        private BaseMenu SelectMenu(UiPanelType menuItem)
        {
            BaseMenu menu = null;
            switch (menuItem)
            {
                case UiPanelType.MainMenu:
                    menu = _mainMenu;
                    break;
                case UiPanelType.OptionsMenu:
                    menu = _optionsMenu;
                    break;
                case UiPanelType.AudioOptions:
                    menu = _audioOptions;
                    break;
                case UiPanelType.MenuPause:
                    menu = _pauseMenu;
                    break;
                default:
                    break;
            }
            return menu;
        }

        public void ReturnToPrevious()
        {
            if (_currentMenu != null)
            { 
                _currentMenu.Hide(); 
                _panelsStack.Pop();
            }

            if (_panelsStack.Count > 0)
            {
                UiPanelType type = _panelsStack.Peek();
                _currentMenu = SelectMenu(type);
                if (_currentMenu != null)
                {
                    _currentMenu.Show();
                }
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

        #endregion
    }
}
