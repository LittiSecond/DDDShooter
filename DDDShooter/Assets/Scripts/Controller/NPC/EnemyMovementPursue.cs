using System;
using UnityEngine;
using UnityEngine.AI;

using Geekbrains;


namespace DddShooter
{
    public sealed class EnemyMovementPursue : EnemyMovement
    {
        #region Fields
        
        private Transform _target;
        private bool _haveTerget;

        #endregion


        #region Properties

        public Transform Target
        {
            set
            {
                _target = value;
                if (_target == null)
                {
                    if (_haveTerget)
                    {
                        StopPursure();
                        _haveTerget = false;
                    }
                }
                else
                {
                    _haveTerget = true;
                }
            }
        }

        #endregion


        #region ClassLifeCycles

        public EnemyMovementPursue(NavMeshAgent agent) : base(agent)
        {
         
        }

        #endregion


        #region Methods
               
        private void StopPursure()
        {
            if (_agent.isActiveAndEnabled)
            {
                _agent.ResetPath();
            }
        }

        #endregion


        #region IExecute

        public override void Execute()
        {
            if (_haveTerget)
            {
                _agent.SetDestination(_target.position);
            }
        }

        #endregion
    }
}
