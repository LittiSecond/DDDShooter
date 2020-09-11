using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Geekbrains
{

    public class OptionsMenu : BaseMenu
    {
        private void LoadVideoOptions()
        {
            UiPanelManager.Execute(UiPanelType.VideoOptions);

        }
        private void LoadSoundOptions()
        {
            UiPanelManager.Execute(UiPanelType.AudioOptions);
        }
        private void LoadGameOptions()
        {
            UiPanelManager.Execute(UiPanelType.GameOptions);
        }
        private void Back()
        {
            UiPanelManager.Execute(UiPanelType.MainMenu);
        }
        public override void Hide()
        {
            if (!IsShow) return;
            IsShow = false;
        }
        public override void Show()
        {
            if (IsShow) return;
            IsShow = true;
        }
    }
}