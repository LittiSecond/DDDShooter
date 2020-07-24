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

        #endregion
        

        #region IExecute

        public virtual void Execute()
        {

        }

        #endregion
    }
}
