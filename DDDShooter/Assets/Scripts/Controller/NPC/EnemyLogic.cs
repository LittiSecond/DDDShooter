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

        private bool _haveTarget;

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
                _body.SubscribeOnEvents(_health.TakeDamage, _health.TakeHealing);
                _health.OnDeathEventHandler += DestroyItSelf;


                if (_agent)
                {
                    _movementPursue = new EnemyMovementPursue(_agent, _settings);

                    _movementPatrol = new EnemyMovementPatrol(_agent, _settings);
                    _movementPatrol.SetPath(body.GetPath());
                    //_movementPatrol.On();
                }

                _enemyVision = new EnemyVision(this, body, _settings);

                if (_settings.HaveRangeAttack)
                {
                    _rangeAttack = new EnemyRangeAttack(_body, _settings);
                }

                //SearchTarget();

                SwithState(NpcState.Patrol);
                On();
            }

            PlayerManager.OnPlayerSpawnedHandler += SearchTarget;
            PlayerManager.OnPlayerDeletedHandler += DisconnectPlayerCharacter;
        }

        #endregion


        #region Methods

        public void PlayerDetected()
        {
            //CustumDebug.Log("EnemyLogic->PlayerDetected:");
            if (_state == NpcState.Patrol || _state == NpcState.Inspection)
            {
                SwithState(NpcState.Pursue);
            }
        }

        public void PlayerLost()
        {
            //CustumDebug.Log("EnemyLogic->PlayerLost:");
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
            PlayerManager.OnPlayerSpawnedHandler -= SearchTarget;
            PlayerManager.OnPlayerDeletedHandler -= DisconnectPlayerCharacter;
            OnDestroyEventHandler?.Invoke(this);
        }

        private void StopLogic()
        {
            if (_movementPursue != null)
            {
                _movementPursue.SetTarget(null);
            }
            if (_movementPatrol != null)
            {
                _movementPatrol.Off();
            }
        }

        private void SearchTarget()
        {
            _haveTarget = PlayerManager.GetPlayerTransform(out _playerTransform);

            if (_haveTarget)
            {
                _movementPursue?.SetTarget(_playerTransform);

                _enemyVision.Target = _playerTransform;

                _rangeAttack?.SetTarget(_playerTransform);
            }
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
                    _enemyVision?.ChangeState(newState);
                    _movementPursue?.Off();
                    _movementPatrol?.On();
                    break;
                case NpcState.Pursue:
                    _enemyVision?.ChangeState(newState);
                    _movementPatrol?.Off();
                    _movementPursue?.On();
                    break;
                default:
                    break;
            }
            _state = newState;
        }

        private void DisconnectPlayerCharacter()
        {
            _haveTarget = false;
            _playerTransform = null;
            _movementPursue?.SetTarget(null);
            _enemyVision.Target = null;
            _rangeAttack?.SetTarget(null);
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
