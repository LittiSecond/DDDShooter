using UnityEngine;

namespace Geekbrains
{
    public sealed class PauseMessageUiText : MonoBehaviour
    {
        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}
