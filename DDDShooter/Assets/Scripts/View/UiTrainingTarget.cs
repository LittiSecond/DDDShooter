using UnityEngine;
using UnityEngine.UI;


namespace DddShooter
{
    public sealed class UiTrainingTarget : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _fullDamageDisplay;
        [SerializeField] private Text _lastDamageDisplay;

        private bool _isDataError;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _isDataError = (_fullDamageDisplay == null || _lastDamageDisplay == null);
        }

        #endregion


        #region Methods

        public void SetDamagInfo(float lastDamag, float fullDamag)
        {
            if (!_isDataError)
            {
                _lastDamageDisplay.text = lastDamag.ToString();
                _fullDamageDisplay.text = fullDamag.ToString();
            }
        }

        #endregion
    }
}