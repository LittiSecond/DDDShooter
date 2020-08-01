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

        private NpcSettings _settings;
        private EnemyHealth _health;
        private EnemyMovementPursue _movementPursue;
        private EnemyMovementPatrol _movementPatrol;
        private EnemyVision _enemyVision;
        private EnemyRangeAttack _rangeAttack;

        private float _changeStateDelay;
        private float _timeCounter;
        private NpcState _state;

        public event Action<EnemyLogic> OnDestroyEventHandler;

        #endregion
 

        #region Properties

        public Transform Transform { get => _body.Transform; }

        #endregion


        #region ClassLifeCycles

        public EnemyLogic(EnemyBody body)
        {

            if (body)
            {
                _body = body;
                _agent = body.gameObject.GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
                _settings = body.Settings;
                if (_settings != null)
                {
                    _changeStateDelay = _settings.ChangeStateDelay;
                }
                else
                {
                    _settings = new NpcSettings();
                }

                _health = new EnemyHealth(_settings);
                _body.SubscribeOnDamagedEvent(_health.TakeDamage);
                _health.OnDeathEventHandler += DestroyItSelf;

                SearchTarget();

                if (_agent)
                {
                    _movementPursue = new EnemyMovementPursue(_agent, _settings);
                    _movementPursue.Target = _playerTransform;

                    _movementPatrol = new EnemyMovementPatrol(_agent, _settings);
                    _movementPatrol.SetPath(body.GetPath());
                    //_movementPatrol.On();
                }

                _enemyVision = new EnemyVision(this, body, _settings);
                _enemyVision.Target = _playerTransform;

                if (_settings.HaveRangeAttack)
                {
                    _rangeAttack = new EnemyRangeAttack(_body, _settings);
                    _rangeAttack.Target = _playerTransform;
                }

                SwithState(NpcState.Patrol);
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
                SwithState(NpcState.Pursue);
            }
        }

        public void PlayerLost()
        {
            CustumDebug.Log("EnemyLogic->PlayerLost:");
            if (_state == NpcState.Pursue)
            {
                _timeCounter = 0.0f;
                SwithState(NpcState.Inspection);
            }
        }

        private void DestroyItSelf()
        {
            StopLogic();
            SwithState(NpcState.Died);
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
                SwithState(NpcState.Patrol);
            }
        }
        
        private void SwithState(NpcState newState )
        {
            switch (newState)
            {
                case NpcState.Patrol:
                    _movementPursue?.Off();
                    _movementPatrol?.On();
                    break;
                case NpcState.Pursue:
                    _movementPatrol?.Off();
                    _movementPursue?.On();
                    break;
                default:
                    break;
            }
            _state = newState;
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
                        _rangeAttack?.Execute();
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
