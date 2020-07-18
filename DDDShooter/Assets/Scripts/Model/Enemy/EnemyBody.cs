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

        [SerializeField] private NpcSettings _npcSettings = new NpcSettings();

        private EnemyPartDamagTranslator[] _damagTranslators;

        #endregion


        #region Properties

        public NpcSettings Settings { get => _npcSettings; }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _damagTranslators = GetComponentsInChildren<EnemyPartDamagTranslator>();

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
        }

        public void SubscribeOnDamagedEvent(Action<float> takerDamag)
        {
            if (takerDamag != null)
            {
                if (_damagTranslators != null)
                {
                    foreach (var t in _damagTranslators)
                    {
                        t.OnDamagedEvent += takerDamag;
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
                return transform;
            }
        }

        #endregion


    }
}
