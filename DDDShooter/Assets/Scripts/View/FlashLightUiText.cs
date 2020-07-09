using UnityEngine;
using UnityEngine.UI;

namespace Geekbrains
{
	public sealed  class FlashLightUiText : MonoBehaviour
	{
		private Text _text;

		private void Awake()
		{
			_text = GetComponent<Text>();
		}

		public float Text
		{
			set => _text.text = $"{value:0.0}";
		}

		public void SetActive(bool value)
		{
			_text.gameObject.SetActive(value);
		}
	}
}