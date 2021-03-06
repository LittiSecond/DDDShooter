﻿using System;
using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public abstract class EnemyBaseLogic : BaseController, IExecute
    {
        #region Fields

        protected EnemyBody _body;
        protected UnityEngine.AI.NavMeshAgent _agent;
        protected Transform _playerTransform;

        protected NpcSettings _settings;
        protected EnemyHealth _health;

        protected float _changeStateDelay;
        protected NpcState _state;

        protected bool _haveTarget;

        public event Action<EnemyBaseLogic> OnDestroyEventHandler;

        #endregion


        #region Properties

        public Transform Transform { get => _body.Transform; }

        #endregion


        #region ClassLifeCycles

        public EnemyBaseLogic(EnemyBody body)
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
 
            }

            PlayerManager.OnPlayerSpawnedHandler += SearchTarget;
            PlayerManager.OnPlayerDeletedHandler += DisconnectPlayerCharacter;
        }

        #endregion


        #region Methods

        protected virtual void DestroyItSelf()
        {
            StopLogic();
            SwithState(NpcState.Died);
            _body.Die();
            Off();
            PlayerManager.OnPlayerSpawnedHandler -= SearchTarget;
            PlayerManager.OnPlayerDeletedHandler -= DisconnectPlayerCharacter;
            OnDestroyEventHandler?.Invoke(this);
        }

        protected virtual void StopLogic()
        {
 
        }

        protected virtual void SearchTarget()
        {

        }

        protected abstract void SwithState(NpcState newState);

        protected virtual void DisconnectPlayerCharacter()
        {
            _playerTransform = null;
        }

        protected void InvokeOnDestroyEventHandler()
        {
            OnDestroyEventHandler?.Invoke(this);
        }

        #endregion


        #region IExecute

        public virtual void Execute()
        {

        }

        #endregion

    }
}
