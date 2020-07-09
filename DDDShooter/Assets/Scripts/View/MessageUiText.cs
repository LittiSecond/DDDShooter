using UnityEngine;
using UnityEngine.UI;

namespace Geekbrains
{
    public sealed class MessageUiText : MonoBehaviour
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

    }
}
