using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    public sealed class MainCameraController : BaseController, IExecute
    {
        #region Fields

        private Transform _cameraTransform;
        private Transform _goFollow;

        #endregion


        #region ClassLifeCycles

        public MainCameraController()
        {
            _cameraTransform = Camera.main.transform;
        }

        #endregion


        #region Methods

        public void Connect(Transform toFollowTransform)
        {
            _goFollow = toFollowTransform;
            if (_goFollow)
            {
                On(null);
            }
        }

        public void Disconnect()
        {
            _goFollow = null;
            Off();
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (IsActive)
            {
                _cameraTransform.position = _goFollow.position;
                _cameraTransform.rotation = _goFollow.rotation;
            }
        }

        #endregion
    }
}
