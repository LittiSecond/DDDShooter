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

        #endregion


        #region ClassLifeCycles

        public EnemyMovement(NavMeshAgent agent)
        {
            _agent = agent;
        }

        #endregion
               

        #region IExecute

        public virtual void Execute()
        {

        }

        #endregion
    }
}
