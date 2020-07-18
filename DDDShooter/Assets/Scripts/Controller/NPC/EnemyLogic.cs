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

        private float _changeStateDelay = 3.0f;
        private float _timeCounter;
        private NpcState _state;

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

                _state = NpcState.Patrol;
                On();
            }
        }

        #endregion


        #region Methods

        public void PlayerDetected()
        {
            CustumDebug.Log("EnemyLogic->PlayerDetected:");
            if (_state == NpcState.Patrol || _state == NpcState.Inspection)
            {
                _state = NpcState.Pursue;
            }
        }

        public void PlayerLost()
        {
            CustumDebug.Log("EnemyLogic->PlayerLost:");
            if (_state == NpcState.Pursue)
            {
                _timeCounter = 0.0f;
                _state = NpcState.Inspection;
            }
        }

        private void DestroyItSelf()
        {
            StopLogic();
            _state = NpcState.Died;
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
        
        private void CountTime()
        {
            _timeCounter += Time.deltaTime;
            if (_timeCounter >= _changeStateDelay)
            {
                _state = NpcState.Patrol;
            }
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (IsActive)
            {
                switch (_state)
                {
                    case NpcState.None:
                        break;
                    case NpcState.Patrol:
                        _movementPatrol?.Execute();
                        _enemyVision?.Execute();
                        break;
                    case NpcState.Pursue:
                        _movementPursue?.Execute();
                        _enemyVision?.Execute();
                        break;
                    case NpcState.Died:
                        break;
                    case NpcState.Inspection:
                        _enemyVision?.Execute();
                        CountTime();
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

    }
}
