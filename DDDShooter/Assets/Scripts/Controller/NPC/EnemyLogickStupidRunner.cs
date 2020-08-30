using System;
using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public class EnemyLogickStupidRunner //: EnemyBaseLogic
    {
        /*
        #region Fields


        private EnemyAnimation _animation;

        private NpcState _state;

        #endregion


        #region Properties

        public Transform Transform { get => _body.Transform; }

        #endregion


        #region ClassLifeCycles

        public EnemyLogickStupidRunner(EnemyBody body)
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

                    _movementPatrol = new EnemyMovementPatrol(_agent, _settings, _body.Transform);
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


        }

        #endregion


        #region Methods



        private void SwithState(NpcState newState)
        {
            switch (newState)
            {


            }
            _state = newState;
        }


        #endregion

        
        #region IExecute

        public void Execute()
        {
            if (IsActive)
            {

            }
        }

        #endregion

        */
    }
}
