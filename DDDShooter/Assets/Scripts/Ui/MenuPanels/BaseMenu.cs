using UnityEngine;
using DddShooter;

namespace Geekbrains
{
    public abstract class BaseMenu : MonoBehaviour
    {
        protected bool IsShow { get; set; }
        protected UiPanelManager UiPanelManager;
        protected virtual void Awake()
        {
            UiPanelManager = FindObjectOfType<UiPanelManager>();
            //Debug.Log("BaseMenu->Awake:");
            //LangManager.Instance.OnLanguageChange += TranslateTexts;
        }

        //private void OnDestroy()
        //{
        //    LangManager.Instance.OnLanguageChange -= TranslateTexts;
        //}


        private void OnEnable()
        {
            LangManager.Instance.OnLanguageChange += TranslateTexts;
        }

        private void OnDisable()
        {
            LangManager.Instance.OnLanguageChange -= TranslateTexts;
        }

       

        public abstract void Hide();
        public abstract void Show();
        protected abstract void TranslateTexts();
    }
}
