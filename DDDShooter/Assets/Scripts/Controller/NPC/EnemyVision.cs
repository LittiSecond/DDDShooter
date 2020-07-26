using System;
using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public class EnemyVision : IExecute
    {
        #region Fields

        private Transform _visionTransform;
        private Transform _playerTransform;
        private EnemyLogic _enemyLogic;

        private float _visionRange;
        private float _visionAngle;

        private bool _haveTerget;
        private bool _isPlayerVisible;

        #endregion


        #region Properties

        public Transform Target
        {
            set
            {
                _playerTransform = value;
                _isPlayerVisible = false;
                _haveTerget = !(_playerTransform == null);
            }
        }

        #endregion


        #region ClassLifeCycles

        public EnemyVision(EnemyLogic logic, EnemyBody body, NpcSettings settings)
        {
            _enemyLogic = logic;
            _visionTransform = body.GetVisionPoint();
            if (settings != null)
            {
                _visionRange = settings.VisionRange;
                _visionAngle = settings.VisionAngle;
            }
        }

        #endregion


        #region Methods

        private bool CheckDistance()
        {
            return (_visionTransform.position - _playerTransform.position).sqrMagnitude < _visionRange * _visionRange;
        }

        private bool CheckAngle()
        {
            float angle = Vector3.Angle(_playerTransform.position - _visionTransform.position, _visionTransform.forward);
            return angle <= _visionAngle;

        }

        private bool CheckPlayerIsBlocked()
        {
            RaycastHit hit;
            if (Physics.Linecast(_visionTransform.position, _playerTransform.position, out hit))
            {
                return hit.transform.GetComponent<PlayerBody>() == null;
                //if (hit.transform == _playerTransform) return false;
            }
            return true;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (_haveTerget)
            {
                bool isVisible = false;
                if (CheckDistance())
                    if (CheckAngle())
                        if (!CheckPlayerIsBlocked())
                        {
                            isVisible = true;
                        }

                if (_isPlayerVisible != isVisible)
                {
                    if (isVisible)
                    {
                        _enemyLogic.PlayerDetected();
                    }
                    else
                    {
                        _enemyLogic.PlayerLost();
                    }
                    _isPlayerVisible = isVisible;
                }
            }
        }

        #endregion
    }
}
