using UnityEngine;
using UnityEngine.UI;

namespace Geekbrains
{
    public class InterfaceResources : MonoBehaviour
    {
        //public ButtonUi ButtonPrefab { get; private set; }
        public Canvas MainCanvas { get; private set; }
        public SliderUI ProgressbarPrefab { get; private set; }
        private void Awake()
        {
            //ButtonPrefab = Resources.Load<ButtonUi>("Button");
            MainCanvas = FindObjectOfType<Canvas>();
            ProgressbarPrefab = Resources.Load<SliderUI>("Progressbar");
        }
    }
}