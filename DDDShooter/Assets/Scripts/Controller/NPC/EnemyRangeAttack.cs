using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    public sealed class EnemyRangeAttack : IExecute
    {
        #region Fields

        private Transform _weaponJoinPoint;
        private Transform _target;
        private Weapon _weapon;

        private float _attackDistance;
        private float _missTargetAngle = 5.0f;

        private bool _isEnabled;
        private bool _haveTarget;


        #endregion


        #region Properties

        public Transform Target
        {
            set
            {
                _target = value;
                _haveTarget = _target != null;
            }
        }

        #endregion


        #region ClassLifeCycles

        public EnemyRangeAttack(EnemyBody body, NpcSettings settings)
        {
            _weaponJoinPoint = body.WeaponJoinPoint;
            if (_weaponJoinPoint)
            {
                _weapon = _weaponJoinPoint.GetComponentInChildren<Weapon>();
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
                Vector3 lookDirection = _weaponJoinPoint.forward;
                Vector3 toTargetDirection = _target.position - _weaponJoinPoint.position;

                float angle = Vector3.Angle(toTargetDirection, lookDirection);
                return angle <= _missTargetAngle;
            }
            else
            {
                return false;
            }
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (_isEnabled && _haveTarget)
            {
                if (CheckTargetInFireLine())
                {
                    _weapon.Fire();
                }
            }
        }

        #endregion
    }
}
