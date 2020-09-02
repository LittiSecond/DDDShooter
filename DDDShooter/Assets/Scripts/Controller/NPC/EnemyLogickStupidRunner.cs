using System;
using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public class EnemyLogickStupidRunner : EnemyBaseLogic
    {
        
        #region Fields


        private NpcAnimationStupidRunner _animation;

        

        #endregion


        #region Properties

        

        #endregion


        #region ClassLifeCycles

        public EnemyLogickStupidRunner(EnemyBody body) : base(body)
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

                    hadMovementScriptsCreated = true;
                }




                Animator animator = _body.BodyAnimator;
                if (animator != null)
                {
                    _animation = new NpcAnimationStupidRunner(animator, _settings);
                    if (hadMovementScriptsCreated)
                    {
                
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


            }
            _state = newState;
        }


        #endregion

        
        #region IExecute

        public override void Execute()
        {
            if (IsActive)
            {

            }
        }

        #endregion

        
    }
}
