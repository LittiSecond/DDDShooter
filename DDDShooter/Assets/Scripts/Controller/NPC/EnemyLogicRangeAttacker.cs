using UnityEngine;


namespace DddShooter
{
    public class EnemyLogicRangeAttacker : EnemyBaseLogic
    {
        #region Fields

        private EnemyMovementPursue _movementPursue;
        private EnemyMovementPatrol _movementPatrol;
        private EnemyVision _enemyVision;
        private EnemyRangeAttack _rangeAttack;
        private EnemyAnimation _animation;

        private float _timeCounter;


        #endregion


        #region Properties

        #endregion


        #region ClassLifeCycles

        public EnemyLogicRangeAttacker(EnemyBody body) : base(body)
        {
            
            if (body)
            {
                _body = body;
                _agent = body.Transform.GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
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

                bool hadMovementScriptsCreated = false;
                if (_agent)
                {
                    _movementPursue = new EnemyMovementPursue(_agent, _settings);

                    _movementPatrol = new EnemyMovementPatrol(_agent, _settings);
                    _movementPatrol.SetPath(body.GetPath());
                    hadMovementScriptsCreated = true;
                }

                _enemyVision = new EnemyVision(this, body, _settings);

                if (_settings.HaveRangeAttack)
                {
                    _rangeAttack = new EnemyRangeAttack(_body, _settings);
                }

                Animator animator = _body.BodyAnimator;
                if (animator != null)
                {
                    _animation = new EnemyAnimation(animator);
                    if (hadMovementScriptsCreated)
                    {
                        _movementPursue.ChangeSpeedHandler += _animation.ChangeSpeed;
                        _movementPatrol.ChangeSpeedHandler += _animation.ChangeSpeed;
                    }
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
            //CustumDebug.Log("EnemyBaseLogic->PlayerDetected:");
            if (_state == NpcState.Patrol || _state == NpcState.Inspection)
            {
                SwithState(NpcState.Pursue);
            }
        }

        public void PlayerLost()
        {
            //CustumDebug.Log("EnemyBaseLogic->PlayerLost:");
            if (_state == NpcState.Pursue)
            {
                _timeCounter = 0.0f;
                SwithState(NpcState.Inspection);
            }
        }

        //protected void DestroyItSelf()
        //{
        //    StopLogic();
        //    SwithState(NpcState.Died);
        //    _body.Die();
        //    Off();
        //    PlayerManager.OnPlayerSpawnedHandler -= SearchTarget;
        //    PlayerManager.OnPlayerDeletedHandler -= DisconnectPlayerCharacter;
        //    InvokeOnDestroyEventHandler();
        //}

        protected override void StopLogic()
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

        protected override void SearchTarget()
        {
            _haveTarget = PlayerManager.GetPlayerTransform(out _playerTransform);

            if (_haveTarget)
            {
                _movementPursue?.SetTarget(_playerTransform);

                _enemyVision.Target = _playerTransform;

                _rangeAttack?.SetTarget(_playerTransform);
            }
            //CustumDebug.Log("EnemyBaseLogic->SearchTarget:");
        }

        private void CountTime()
        {
            _timeCounter += Time.deltaTime;
            if (_timeCounter >= _changeStateDelay)
            {
                SwithState(NpcState.Patrol);
            }
        }

        protected override void SwithState(NpcState newState)
        {
            switch (newState)
            {
                case NpcState.Patrol:
                    _enemyVision?.ChangeState(newState);
                    _movementPursue?.Off();
                    _movementPatrol?.On();
                    _animation?.ChangeState(NpcAnimationState.Walk);
                    break;
                case NpcState.Pursue:
                    _enemyVision?.ChangeState(newState);
                    _movementPatrol?.Off();
                    _movementPursue?.On();
                    _animation?.ChangeState(NpcAnimationState.WalkAim);
                    break;
                case NpcState.Died:
                    _animation?.ChangeState(NpcAnimationState.Died);
                    break;
                case NpcState.Inspection:
                    _animation?.ChangeState(NpcAnimationState.Inspection);
                    break;
                default:
                    break;
            }
            _state = newState;
            //CustumDebug.Log("EnemyBaseLogic->SwithState: _state = " + _state.ToString());
        }

        protected override void DisconnectPlayerCharacter()
        {
            _haveTarget = false;
            _playerTransform = null;
            _movementPursue?.SetTarget(null);
            _enemyVision.Target = null;
            _rangeAttack?.SetTarget(null);
            PlayerLost();
            //CustumDebug.Log("EnemyBaseLogic->DisconnectPlayerCharacter:");
        }

        #endregion


        #region IExecute

        public override void Execute()
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
