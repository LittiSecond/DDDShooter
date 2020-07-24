using UnityEngine;
using UnityEngine.AI;


namespace DddShooter
{
    public sealed class EnemyMovementPursue : EnemyMovement
    {
        #region Fields
        
        private Transform _target;
        private float _stopDistance;
        private bool _haveTarget;

        #endregion


        #region Properties

        public Transform Target
        {
            set
            {
                _target = value;
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
        }

        #endregion


        #region ClassLifeCycles

        public EnemyMovementPursue(NavMeshAgent agent, NpcSettings settings) : base(agent)
        {
         if (settings != null)
            {
                _stopDistance = settings.PursueStopDistance;
            }
        }

        #endregion


        #region Methods

        public override void On()
        {
            base.On();
            if (_haveTarget)
            {
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

        #endregion


        #region IExecute

        public override void Execute()
        {
            if (_isEnabled && _haveTarget)
            {
                _agent.SetDestination(_target.position);
            }
        }

        #endregion
    }
}
