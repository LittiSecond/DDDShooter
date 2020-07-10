using UnityEngine;
using UnityEngine.UI;


namespace DddShooter
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
