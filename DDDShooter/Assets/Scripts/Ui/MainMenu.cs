﻿using UnityEngine;
using UnityEngine.SceneManagement;

using DddShooter;


namespace Geekbrains
{

    public class MainMenu : BaseMenu
    {

        [SerializeField] private GameObject _mainPanel;

        [SerializeField] private ButtonUi _newGame;
        [SerializeField] private ButtonUi _options;
        [SerializeField] private ButtonUi _quit;

        private void Start()
        {
            _newGame.GetText.text = LangManager.Instance.Text(
                TextConstants.MENU_ITEMS_GROUP_ID, TextConstants.NEW_GAME_TEXT_ID);
            _newGame.GetControl.onClick.AddListener(delegate
            {
                LoadNewGame(SceneManagerHelper.Instance.Scenes.GameScene.SceneAsset.name);
            });

            _options.GetText.text = LangManager.Instance.Text(
                TextConstants.MENU_ITEMS_GROUP_ID, TextConstants.OPTIONS_TEXT_ID);
            _options.SetInteractable(false);

            _quit.GetText.text = LangManager.Instance.Text(
                 TextConstants.MENU_ITEMS_GROUP_ID, TextConstants.QUIT_TEXT_ID);
            _quit.GetControl.onClick.AddListener(delegate
            {
                UiPanelManager.QuitGame();
            });

            //for (int i = 1; i <= 4; i++)
            //{
            //    LangManager.Instance.Text("Dialog", $"{i}");
            //}
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

        private void ShowOptions()
        {
            UiPanelManager.Execute(UiPanelType.OptionsMenu);
        }

        private void LoadNewGame(string lvl)
        {
            Debug.Log("MainMenu::LoadNewGame: lvl = " + lvl);
            SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
            SceneManagerHelper.Instance.LoadSceneAsync(lvl);
        }

        private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            // init game не требуется

            SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
        }
    }
}