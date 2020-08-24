using UnityEngine;
using UnityEngine.AI;

using Geekbrains;


namespace DddShooter
{
    public sealed class EnemyMovementPursue : EnemyMovement
    {
        #region Fields

        private Transform _bodyTransforn;
        private Transform _target;
        private Vector3 _targetLastPosition;

        private float _pursueSpeed;
        private float _stopDistance;
        private float _rotationSpeedRadSec;

        private bool _haveTarget;

        //private float _timeCounter;
        //private readonly float _updateDestinationInterval = 0.25f;
        private const float NO_MOVE_SPEED = 0.001f;

        #endregion


        #region ClassLifeCycles

        public EnemyMovementPursue(NavMeshAgent agent, NpcSettings settings) : base(agent)
        {
            if (settings != null)
            {
                _pursueSpeed = settings.PursueSpeed;
                _stopDistance = settings.PursueStopDistance;
            }
            _bodyTransforn = _agent.transform;
            _rotationSpeedRadSec = _agent.angularSpeed * Mathf.Deg2Rad;
        }

        #endregion


        #region Methods

        public override void On()
        {
            base.On();
            if (_haveTarget)
            {
                _agent.speed = _pursueSpeed;
                _agent.stoppingDistance = _stopDistance;
            }
        }

        private void StopPursure()
        {
            if (_agent.isActiveAndEnabled)
            {
                _agent.ResetPath();
            }
        }

        private bool IsMove()
        {
            Vector3 speed = _agent.velocity;
            return (Mathf.Abs(speed.x) > NO_MOVE_SPEED || Mathf.Abs(speed.y) > NO_MOVE_SPEED);
        }

        private void Rotate()
        {
            Vector3 targetDir = _targetLastPosition - _bodyTransforn.position;
            targetDir.y = 0;

            Vector3 newDir = Vector3.RotateTowards(_bodyTransforn.forward, targetDir,
                _rotationSpeedRadSec * Time.deltaTime, 0.0f);
            _bodyTransforn.rotation = Quaternion.LookRotation(newDir);

            //Debug.Log("EnemyMovementPursue->Rotate: targetDir = " + targetDir.ToString());
            //_bodyTransforn.Rotate()
        }

        public void SetTarget(Transform terget)
        {
            _target = terget;
            if (_target == null)
            {
                if (_haveTarget)
                {
                    StopPursure();
                    _haveTarget = false;
                }
            }
            else
            {
                _haveTarget = true;
            }
        }

        #endregion


        #region IExecute

        public override void Execute()
        {
            if (_isEnabled && _haveTarget)
            {
                _targetLastPosition = _target.position;
                _agent.SetDestination(_targetLastPosition);
                if (!IsMove())
                {
                    Rotate();
                }

                //CustumDebug.Log("EnemyMovementPursue->Execute: _isStopped = " + _isStopped.ToString());
            }
        }

        #endregion
    }
}
