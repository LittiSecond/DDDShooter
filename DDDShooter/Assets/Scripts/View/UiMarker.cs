using UnityEngine;
using UnityEngine.UI;


namespace DddShooter
{
    public sealed class UiMarker : MonoBehaviour
    {

        #region Fields

        private RectTransform _rectTransform;

        #endregion


        #region Properties

        //public RectTransform RectTransform 

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _rectTransform = transform as RectTransform;
        }

        #endregion


        #region Methods

        public void SetPosition(Vector2 pos)
        {
            pos = new Vector2(Mathf.Clamp(pos.x, 0, 1), Mathf.Clamp(pos.y, 0, 1));
            _rectTransform.anchorMax = pos;
            _rectTransform.anchorMin = pos;
        }

        public void SetAngle(float angle)
        {
            //_rectTransform.Rotate(new Vector3(0, 0, angle));
            Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle)); //= _rectTransform.rotation;
            //q.eulerAngles = ;
            _rectTransform.rotation = q;
        }

        #endregion
    }
}
