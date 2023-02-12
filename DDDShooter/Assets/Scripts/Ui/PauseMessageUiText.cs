using UnityEngine;


namespace DddShooter
{
    public sealed class PauseMessageUiText : MonoBehaviour
    {
        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}
