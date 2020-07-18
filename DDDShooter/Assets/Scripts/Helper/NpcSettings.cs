using System;
using UnityEngine;


namespace DddShooter
{
    [Serializable]
    public sealed class NpcSettings
    {
        #region Fields

        [SerializeField] private float _maxHealth = 100.0f;
        [SerializeField] private float _patrolStopDuration = 3.0f;
        [SerializeField] private float _pursueStopDistance = 2.0f;
        [SerializeField] private float _changeStateDelay = 3.0f;
        [SerializeField] private float _visionRange = 20.0f;
        [SerializeField] private float _visionAngle = 80.0f;

        #endregion


        #region Properties

        public float MaxHealth { get => _maxHealth; }

        public float PatrolStopDuration { get => _patrolStopDuration; }

        public float PursueStopDistance { get => _pursueStopDistance; }

        public float ChangeStateDelay { get => _changeStateDelay; }

        public float VisionRange { get => _visionRange; }

        public float VisionAngle { get => _visionAngle; }

        #endregion
    }
}
