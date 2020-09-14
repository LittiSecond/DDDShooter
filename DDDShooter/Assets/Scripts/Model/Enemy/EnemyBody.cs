using System;
using System.Collections.Generic;
using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    public sealed class EnemyBody : BaseObjectScene
    {
        #region Fields

        [SerializeField] private PatrolPath _patrolPath;
        [SerializeField] private Transform _visionPoint;
        [SerializeField] private Transform _weaponJoinPoint;

        [SerializeField] private NpcSettings _npcSettings = new NpcSettings();

        private EnemyPartDamagTranslator[] _damagTranslators;
        private Weapon _weapon;
        private Animator _animator;
        
        #endregion


        #region Properties

        public NpcSettings Settings { get => _npcSettings; }
        public Transform WeaponJoinPoint { get => _weaponJoinPoint; }
        public Animator BodyAnimator { get => _animator; }


        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _damagTranslators = GetComponentsInChildren<EnemyPartDamagTranslator>();
            _weapon = GetComponentInChildren<Weapon>();
            _animator = Transform.GetComponentInChildren<Animator>();
        }

        #endregion


        #region Methods

        public void Die()
        {
            if (_damagTranslators != null)
            {
                foreach (var t in _damagTranslators)
                {
                    t.Die();
                }
            }
            if (_weapon)
            {
                _weapon.JoinTo(null);
                _weapon.EnablePhysics();
                _weapon.DisableEndlessAmmunition();
            }
        }

        public void SubscribeOnEvents(Action<float> takerDamag, Action<float> takerHealing)
        {
            if (takerDamag != null)
            {
                if (_damagTranslators != null)
                {
                    foreach (var t in _damagTranslators)
                    {
                        t.OnDamagedEvent += takerDamag;
                        t.OnHealingEvent += takerHealing;
                    }
                }
            }
        }

        public List<Vector3> GetPath()
        {
            if (_patrolPath)
            {
                return _patrolPath.GetPath();
            }
            else
            {
                return null;
            }
        }

        public Transform GetVisionPoint()
        {
            if (_visionPoint)
            {
                return _visionPoint;
            }
            else
            {
                CustumDebug.LogError("EnemyBody->GetVisionPoint: _visionPoint == null");
                return Transform;
            }
        }

        #endregion


    }
}
