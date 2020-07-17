using System;
using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public class EnemyLogic : BaseController, IExecute
    {
        #region Fields

        private EnemyBody _body;
        private UnityEngine.AI.NavMeshAgent _agent;
        private Transform _playerTransform;

        private EnemyHealth _health;
        private EnemyMovementPursue _movementPursue;
        private EnemyMovementPatrol _movementPatrol;
        private EnemyVision _enemyVision;

        #endregion

        public event Action<EnemyLogic> OnDestroyEventHandler;

        #region ClassLifeCycles

        public EnemyLogic(EnemyBody body)
        {
            if (body)
            {
                _body = body;
                _agent = body.gameObject.GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();

                _health = new EnemyHealth();
                _body.SubscribeOnDamagedEvent(_health.TakeDamage);
                _health.OnDeathEventHandler += DestroyItSelf;

                SearchTarget();

                if (_agent)
                {
                    _movementPursue = new EnemyMovementPursue(_agent);
                    _movementPursue.Target = _playerTransform;

                    _movementPatrol = new EnemyMovementPatrol(_agent);
                    _movementPatrol.SetPath(body.GetPath());
                    _movementPatrol.On();
                }

                _enemyVision = new EnemyVision(this, body);
                _enemyVision.Target = _playerTransform;

                On();
            }
        }

        #endregion


        #region Methods

        public void PlayerDetected()
        {
            CustumDebug.Log("EnemyLogic->PlayerDetected:");
        }

        public void PlayerLost()
        {
            CustumDebug.Log("EnemyLogic->PlayerLost:");
        }

        private void DestroyItSelf()
        {
            StopLogic();
            _body.Die();
            Off();
            OnDestroyEventHandler?.Invoke(this);
        }

        private void StopLogic()
        {
            if (_movementPursue != null)
            {
                _movementPursue.Target = null;
            }
            if (_movementPatrol != null)
            {
                _movementPatrol.Off();
            }
        }

        private void SearchTarget()
        {
            CharacterController controller = ServiceLocatorMonoBehaviour.GetService<CharacterController>();
            _playerTransform = controller.transform;
        }


        #endregion


        #region IExecute

        public void Execute()
        {
            if (IsActive)
            {
                //_movementPursue?.Execute();
                _movementPatrol?.Execute();
                _enemyVision?.Execute();
            }
        }

        #endregion

    }
}
