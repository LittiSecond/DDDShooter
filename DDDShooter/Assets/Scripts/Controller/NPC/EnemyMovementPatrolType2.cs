using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace DddShooter
{
    public sealed class EnemyMovementPatrolType2 : EnemyMovement
    {
        #region Fields

        private EnemyMovementPatrol _patrol;

        private float[] _speeds;
        private float _maxSpeed;
        private float _currentSpeed;

        private int _speedStep;


        private readonly int _speedStepQuantity = 4;

        //public event Action<float> ChangeSpeedHandler;

        #endregion


        #region ClassLifeCycles

        public EnemyMovementPatrolType2(NavMeshAgent agent, NpcSettings settings) : base(agent)
        {
            if (settings != null)
            {
                _maxSpeed = settings.PatrolSpeed;
                _currentSpeed = 0;
                _patrol = new EnemyMovementPatrol(agent, settings);
                _patrol.ChangeSpeedHandler += ChangeSpeed;
            }

            prepareSpeedChangingParameters();
            //Debug.Log("EnemyMovementPatrolType2->EnemyMovementPatrolType2:");
        }

        #endregion


        #region Methods

        public void SetPath(List<Vector3> newPath)
        {
            _patrol.SetPath(newPath);
        }

        public override void On()
        {
            base.On();
            _patrol.On();
            //Debug.Log("EnemyMovementPatrolType2->On:");
        }

        private void ChangeSpeed(float newSpeed)
        {
            InvokeChangeSpeedEvent(newSpeed);
            if (newSpeed > float.Epsilon)
            {
                selectNextSpeed();
            }
        }

        private void prepareSpeedChangingParameters()
        {
            _speeds = new float[_speedStepQuantity];
            float speedStep = _maxSpeed / _speedStepQuantity;
            for (int i = 0; i < _speedStepQuantity; i++)
            {
                _speeds[i] = (i + 1) * speedStep;
            }
            _speedStep = -1;
        }

        private void selectNextSpeed()
        {
            _speedStep++;
            if (_speedStep >= _speedStepQuantity)
            {
                _speedStep = 0;
            }

            _currentSpeed = _speeds[_speedStep];
            _agent.speed = _currentSpeed;
            InvokeChangeSpeedEvent(_currentSpeed);
        }

        #endregion


        #region IExecute

        public override void Execute()
        {
            if (_isEnabled)
            {
                _patrol.Execute();
            }
        }

        #endregion
    }
}
