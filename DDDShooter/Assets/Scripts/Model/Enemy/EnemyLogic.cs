using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class EnemyLogic : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _target;

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
        }

        #endregion


        #region Methods

        private void UpdateDestination()
        {
            _agent.SetDestination(_target.position);
        }

        #endregion



    }
}