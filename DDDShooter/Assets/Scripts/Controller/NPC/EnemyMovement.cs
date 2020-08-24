using System;
using UnityEngine;
using UnityEngine.AI;

using Geekbrains;


namespace DddShooter
{
    public abstract class EnemyMovement : IExecute
    {
        #region Fields

        protected NavMeshAgent _agent;
        protected bool _isEnabled;

        public event Action<float> ChangeSpeedHandler;

        #endregion


        #region ClassLifeCycles

        public EnemyMovement(NavMeshAgent agent)
        {
            _agent = agent;
        }

        #endregion


        #region Methods

        public virtual void On()  // в BaseController есть ненужный здесь функционал, поэтому не наследую
        {
            _isEnabled = true;
        }

        public virtual void Off()
        {
            _isEnabled = false;
            if (_agent.isActiveAndEnabled)
            {
                _agent.ResetPath();
            }
        }

        protected void InvokeChangeSpeedEvent(float speed)
        {
            ChangeSpeedHandler?.Invoke(speed);
            // из-за того, что эвент нельзя вызвать в наследниках, пришлось создать этот метод
        }

        #endregion


        #region IExecute

        public virtual void Execute()
        {

        }

        #endregion
    }
}
