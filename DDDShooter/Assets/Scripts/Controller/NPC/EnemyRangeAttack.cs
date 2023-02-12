using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    public sealed class EnemyRangeAttack : IExecute
    {
        #region Fields

        private Transform _weaponJoinPoint;
        private Transform _weaponFixPoint;
        private Transform _target;
        private Weapon _weapon;

        private Vector3 _targetLastPosition;

        private float _attackDistance;
        private float _missTargetAngle = 5.0f;
        private float _verticalAngleOffset;

        private bool _isEnabled;
        private bool _haveTarget;
        private bool _haveWeapon;

        #endregion


        #region ClassLifeCycles

        public EnemyRangeAttack(EnemyBody body, NpcRangeAttacker settings)
        {
            _weaponJoinPoint = body.WeaponJoinPoint;
            if (_weaponJoinPoint)
            {
                _weaponFixPoint = _weaponJoinPoint.parent;
                _weapon = _weaponJoinPoint.GetComponentInChildren<Weapon>();
                _haveWeapon = _weapon != null;
            }

            if (settings != null)
            {
                _attackDistance = settings.RangeAttackDistance;
            }

            _isEnabled = _weapon != null;
        }

        #endregion


        #region Methods

        private bool CheckTargetInFireLine()
        {
            if (_haveTarget)
            {
                Vector3 lookDirection = _weaponFixPoint.forward;
                Vector3 toTargetDirection = _targetLastPosition - _weaponFixPoint.position;

                float angle = Vector3.Angle(toTargetDirection, lookDirection);
                return angle <= _missTargetAngle;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Доворачивает оружие на цель в небольших пределах. Пока доворачивает только по вертикали.
        /// </summary>
        private void RotateWeaponToTarget()
        {
            if (_haveWeapon)
            {
                Vector3 weaponPos = _weaponJoinPoint.position;
                Vector3 targetDir = _targetLastPosition - weaponPos;
                Vector3 weaponDir = _weaponFixPoint.forward;

                Vector3 relativTargetDir = _weaponFixPoint.InverseTransformDirection(targetDir);
                relativTargetDir.x = 0;
                float xAngle = Vector3.Angle(Vector3.forward, relativTargetDir);
                if (_targetLastPosition.y > weaponPos.y)
                {
                    xAngle = -xAngle;
                }
                _weaponJoinPoint.localEulerAngles = new Vector3(xAngle, 0, 0);
                //Debug.DrawRay(_weaponJoinPoint.position, _weaponJoinPoint.forward * 20.0f, Color.red);
                //Debug.DrawRay(_weaponFixPoint.position, _weaponFixPoint.forward * 20.0f, Color.blue);
            }
        }

        public void SetTarget(Transform target)
        {
            _target = target;
            _haveTarget = _target != null;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (_isEnabled && _haveTarget)
            {
                _targetLastPosition = _target.position;

                RotateWeaponToTarget();

                if (CheckTargetInFireLine())
                {
                    _weapon.Fire();
                }
            }
        }

        #endregion
    }
}
