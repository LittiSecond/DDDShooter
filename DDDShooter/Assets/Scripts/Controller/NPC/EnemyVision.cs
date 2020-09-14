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
        private EnemyLogicRangeAttacker _enemyLogic;

        private LayerMask _mask;

        private float _visionRange;
        private float _visionAngle;

        private bool _haveTerget;
        private bool _isPlayerVisible;
        private bool _isVisionAllAround;

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

        public EnemyVision(EnemyLogicRangeAttacker logic, EnemyBody body, NpcSettings settings)
        {
            _visionTransform = body.GetVisionPoint();
            if (settings != null)
            {
                _visionRange = settings.VisionRange;
                _visionAngle = settings.VisionAngle;
            }
            _enemyLogic = logic;
            _mask = LayerManager.GetLayerMask(LayerManager.Layer.Default, LayerManager.Layer.Ground, 
                LayerManager.Layer.MoveableObjects, LayerManager.Layer.Player);
        }

        #endregion


        #region Methods
        
        public void ChangeState(NpcState newState)
        {
            switch (newState)
            {
                case NpcState.Patrol:
                    _isVisionAllAround = false;
                    break;
                case NpcState.Pursue:
                    _isVisionAllAround = true;
                    break;
                default:
                    break;
            }
        }

        private bool CheckDistance()
        {
            return (_visionTransform.position - _playerTransform.position).sqrMagnitude < _visionRange * _visionRange;
        }

        private bool CheckAngle()
        {
            if (_isVisionAllAround)
            {
                return true;
            }
            float angle = Vector3.Angle(_playerTransform.position - _visionTransform.position, _visionTransform.forward);
            return angle <= _visionAngle;
        }

        private bool CheckPlayerIsBlocked()
        {
            RaycastHit hit;
            if (Physics.Linecast(_visionTransform.position, _playerTransform.position, out hit, _mask))
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
