﻿using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class EnemyLogic : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _target;

        private EnemyHealth _enemyHealth;
        private UnityEngine.AI.NavMeshAgent _agent;
        private ITimeRemaining _timeRemaining;

        private float _updateInterval = 0.5f;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            _timeRemaining = new TimeRemaining(UpdateDestination, _updateInterval, true);
            if (_target)
            {
                _timeRemaining.AddTimeRemaining();
            }
            _enemyHealth = GetComponent<EnemyHealth>();
                {
                if (_enemyHealth)
                {
                    _enemyHealth.OnDeathEvent += StopLogic;
                }
            }
        }

        private void OnDisable()
        {
            StopLogic();
        }

        #endregion


        #region Methods

        private void UpdateDestination()
        {
            _agent.SetDestination(_target.position);
        }

        private void StopLogic()
        {
            if (_agent.isActiveAndEnabled)
            {
                _agent.ResetPath();
            }
            _timeRemaining.RemoveTimeRemaining();
            if (_enemyHealth)
            {
                _enemyHealth.OnDeathEvent -= StopLogic;
            }
        }

        #endregion



    }
}