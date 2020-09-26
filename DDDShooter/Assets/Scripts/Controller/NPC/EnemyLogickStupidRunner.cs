using System;
using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public class EnemyLogickStupidRunner : EnemyBaseLogic
    {
        
        #region Fields

        private NpcAnimationStupidRunner _animation;
        private EnemyMovementPatrolType2 _movementPatrol;
        private NpcStupidRunner _settings;

        private float _timeCounter;


        #endregion


        #region Properties



        #endregion


        #region ClassLifeCycles

        public EnemyLogickStupidRunner(EnemyBody body) : base(body)
        {

            if (body)
            {
                _settings = body.Settings as NpcStupidRunner;
                bool hadMovementScriptsCreated = false;
                if (_agent)
                {
                    _movementPatrol = new EnemyMovementPatrolType2(_agent, _settings);
                    _movementPatrol.SetPath(body.GetPath());
                    hadMovementScriptsCreated = true;
                }

                Animator animator = _body.BodyAnimator;
                if (animator != null)
                {
                    _animation = new NpcAnimationStupidRunner(animator, _settings);
                    if (hadMovementScriptsCreated)
                    {
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



        protected override void SwithState(NpcState newState)
        {
            switch (newState)
            {
                case NpcState.Patrol:
                    _movementPatrol.On();
                    break;
                case NpcState.Died:
                    _movementPatrol.Off();
                    break;
                default:

                    break;
            }
            _state = newState;
        }

        private void CountTime()
        {
            _timeCounter += Time.deltaTime;
            if (_timeCounter >= _changeStateDelay)
            {
                SwithState(NpcState.Patrol);
            }
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
                        break;
                    case NpcState.Died:
                        break;
                    case NpcState.Inspection:
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
