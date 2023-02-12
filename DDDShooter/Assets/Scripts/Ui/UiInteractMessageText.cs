using UnityEngine;
using UnityEngine.UI;

using Geekbrains;


namespace DddShooter
{
    public sealed class UiInteractMessageText : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        public string Text
        {
            set => _text.text = value;
        }

        public void ClearText()
        {
            _text.text = string.Empty;
        }

    }
}
