using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Geekbrains;


namespace DddShooter
{
    public sealed class EnemyMovementPatrol : EnemyMovement
    {
        #region Fields

        private List<Vector3> _patrolPath;        

        private float _stopDuration;
        private float _timeCounter;
        private int _pathPointIndex = -1;
        private bool _isPhaseMove;
        private bool _isEnabled;

        #endregion


        #region Properties

        #endregion


        #region ClassLifeCycles

        public EnemyMovementPatrol(NavMeshAgent agent, NpcSettings settings) : base(agent)
        {
            _isPhaseMove = false;
            if (settings != null)
            {
                _stopDuration = settings.PatrolStopDuration;
            }      
        }

        #endregion


        #region Methods

        public void SetPath(List<Vector3> newPath)
        {
            _patrolPath = newPath;
            ResetPath();
        }


        #endregion


        #region IExecute

        public override void Execute()
        {
            if (_isEnabled)
            {
                if (_isPhaseMove)
                {
                    if (!_agent.hasPath)
                    {
                        _isPhaseMove = false;
                    }
                }
                else
                {
                    _timeCounter += Time.deltaTime;
                    if (_timeCounter >= _stopDuration)
                    {
                        _timeCounter = 0.0f;

                        _pathPointIndex++;
                        if (_pathPointIndex >= _patrolPath.Count)
                        {
                            _pathPointIndex = 0;
                        }
                        Vector3 destination = _patrolPath[_pathPointIndex];
                        _agent.SetDestination(destination);
                        _isPhaseMove = true;
                    }
                }
            }
        }

        public void On()  // в BaseController есть ненужный здесь функционал, поэтому не наследую
        {
            if (_patrolPath != null)
                if (_patrolPath.Count > 0)
                {
                    _isEnabled = true;
                    _agent.stoppingDistance = 0.0f;
                }
        }

        public void Off()
        {
            _isEnabled = false;
            _agent.ResetPath();
        }

        private void ResetPath()
        {
            _pathPointIndex = -1;
            if (_isEnabled)
            {
                if (_patrolPath == null)
                {
                    _isEnabled = false;
                }
                else if (_patrolPath.Count == 0)
                {
                    _isEnabled = false;
                }
            }
            
        }
        #endregion


    }
}
