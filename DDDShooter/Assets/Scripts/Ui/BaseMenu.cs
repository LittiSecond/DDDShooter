using UnityEngine;


namespace Geekbrains
{
    public abstract class BaseMenu : MonoBehaviour
    {
        protected bool IsShow { get; set; }
        protected UiPanelManager UiPanelManager;
        protected virtual void Awake()
        {
            UiPanelManager = FindObjectOfType<UiPanelManager>();
        }

        public abstract void Hide();
        public abstract void Show();
    }
}
