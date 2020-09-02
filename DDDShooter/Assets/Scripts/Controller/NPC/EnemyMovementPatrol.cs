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

        //private Transform _bodyTransform;
        private List<Vector3> _patrolPath;

        private float _patrolSpeed;
        private float _stopDuration;
        private float _timeCounter;
        private int _pathPointIndex = -1;
        private bool _isPhaseMove;

        #endregion


        #region Properties

        #endregion


        #region ClassLifeCycles

        public EnemyMovementPatrol(NavMeshAgent agent, NpcSettings settings, Transform bodyTransform) : base(agent)
        {
            //_bodyTransform = bodyTransform;
            _isPhaseMove = false;
            if (settings != null)
            {
                _patrolSpeed = settings.PatrolSpeed;
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

        public override void On()  // в BaseController есть ненужный здесь функционал, поэтому не наследую
        {
            if (_patrolPath != null)
                if (_patrolPath.Count > 0)
                {
                    _isEnabled = true;
                    _agent.stoppingDistance = 0.0f;
                    _agent.speed = _patrolSpeed;
                    _pathPointIndex = FindNearestPathPoint();
                    _agent.SetDestination(_patrolPath[_pathPointIndex]);
                    if (_isPhaseMove)
                    {
                        InvokeChangeSpeedEvent(_patrolSpeed);
                    }
                    else
                    {
                        InvokeChangeSpeedEvent(0.0f);
                    }
                }
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

        private int FindNearestPathPoint()
        {
            int pathIndex = -1;
            if (_patrolPath.Count > 1)
            {
                NavMeshPath path = new NavMeshPath();
                float leastLength = float.MaxValue;

                for (int i = 0; i < _patrolPath.Count; i++)
                {
                    if (_agent.CalculatePath(_patrolPath[i], path))
                    {
                        float length = CalculatePathLength(path);
                        if (length < leastLength )
                        {
                            leastLength = length;
                            pathIndex = i;
                        }
                    }
                }
            }

            return pathIndex;
        }

        private float CalculatePathLength(NavMeshPath path)
        {
            float length = 0;
            Vector3 previousCorner = path.corners[0];
            for (int i = 1; i < path.corners.Length; i++)
            {
                Vector3 currentCorner = path.corners[i];
                length += (currentCorner - previousCorner).magnitude;
                previousCorner = path.corners[i];
            }

            return length;
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
                        InvokeChangeSpeedEvent(0.0f);
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
                        InvokeChangeSpeedEvent(_patrolSpeed);
                    }
                }
            }
        }

        #endregion
    }
}
