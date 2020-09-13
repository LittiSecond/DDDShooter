using UnityEngine;

using DddShooter;


namespace Geekbrains
{
    public class CreateInterface : MonoBehaviour
    {
#if UNITY_EDITOR
        #region Editor
        public void CreateMainMenu()
        {
            CreateComponent();
            gameObject.AddComponent<MainMenu>();
            gameObject.AddComponent<OptionsMenu>();
            Clear();
        }

        public void CreateMenuPause()
        {
            CreateComponent();
            gameObject.AddComponent<PauseMenu>();
            gameObject.AddComponent<OptionsMenu>();
            Clear();
        }

        private void Clear()
        {
            DestroyImmediate(this);
        }

        private void CreateComponent()
        {
            gameObject.AddComponent<UiPanelManager>();
            gameObject.AddComponent<InterfaceResources>();
        }
        #endregion
#endif
    }
}
