using System;
using UnityEngine;

namespace DddShooter
{
    public static class PlayerManager
    {
        #region Fields

        private static Transform _playerTransform;

        private static bool _isPlayerExist;

        public static event Action OnPlayerDeletedHandler;
        public static event Action OnPlayerSpawnedHandler;

        #endregion


        #region Methods

        public static bool GetPlayerTransform(out Transform outTransform)
        {
            if (_isPlayerExist)
            {
                outTransform = _playerTransform;
                return true;
            }
            else
            {
                outTransform = null;
                return false;
            }
        }

        public static void SetPlayerTransform(Transform transform)
        {
            _playerTransform = transform;
            _isPlayerExist = transform != null;
            if (_isPlayerExist)
            {
                OnPlayerSpawnedHandler?.Invoke();
            }
        }

        public static void PlayerDestroyed()
        {
            _playerTransform = null;
            _isPlayerExist = false;
            OnPlayerDeletedHandler?.Invoke();
        }

        #endregion
    }
}
