using UnityEngine;
using UnityEngine.SceneManagement;

using Geekbrains;


namespace DddShooter
{
    public class PauseMenu : BaseMenu
    {
        [SerializeField] private GameObject _menuPanel;

        [SerializeField] private ButtonUi _continue;
        [SerializeField] private ButtonUi _options;
        [SerializeField] private ButtonUi _quit;

        private void Start()
        {
            Debug.Log("PauseMenu::Start:");

            _continue.GetText.text = LangManager.Instance.Text("MenuItems", "Continue");
            _continue.GetControl.onClick.AddListener(ContinueGame);

            //_continue.SetInteractable(false);

            _options.GetText.text = LangManager.Instance.Text("MenuItems", "Options");
            _options.SetInteractable(false);

            _quit.GetText.text = LangManager.Instance.Text("MenuItems", "Quit");
            _quit.GetControl.onClick.AddListener(delegate
            {
                FinishGame(SceneManagerHelper.Instance.Scenes.MainMenuScene.SceneAsset.name);
            });

            IsShow = true;
            Hide();
        }

        public override void Hide()
        {
            Debug.Log("PauseMenu::Hide: IsShow = " + IsShow.ToString());
            if (!IsShow) return;
            _menuPanel.gameObject.SetActive(false);
            IsShow = false;
        }

        public override void Show()
        {
            Debug.Log("PauseMenu::Show: IsShow = " + IsShow.ToString());
            if (IsShow) return;
            _menuPanel.gameObject.SetActive(true);
            IsShow = true;
        }

        private void ContinueGame()
        {
            //Debug.Log("PauseMenu::ContinueGame: IsShow = " + IsShow.ToString());
            //Hide();
            ServiceLocator.Resolve<PauseController>().SwithPause();
        }

        private void ShowOptions()
        {
            UiPanelManager.Execute(UiPanelType.OptionsMenu);
        }

        private void FinishGame(string lvl)
        {
            ServiceLocator.Resolve<PauseController>().SwithPause();
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("PauseMenu::FinishGame: lvl = " + lvl);
            SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
            SceneManagerHelper.Instance.LoadSceneAsync(lvl);
        }

        private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
        }

    }
}
