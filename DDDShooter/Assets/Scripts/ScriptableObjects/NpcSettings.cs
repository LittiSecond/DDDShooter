using UnityEngine;


namespace DddShooter
{
    //[CreateAssetMenu(fileName = "npcSettings", menuName =
    //"CreateScriptableObject/OrbSettings", order = 1)]
    public abstract class NpcSettings : ScriptableObject
    {
        public float MaxHealth = 100.0f;
        public float PatrolStopDuration = 3.0f;
        public float Speed = 3.0f;
        public float VisionRange = 20.0f;
        public float VisionAngle = 80.0f;
        public float ChangeStateDelay = 3.0f;

        /*
        #region Fields

        [SerializeField] private NpcType _npcType = NpcType.RangeAttack;
        [SerializeField] private float _maxHealth = 100.0f;  -
        [SerializeField] private float _patrolSpeed = 3.0f;  -
        [SerializeField] private float _patrolStopDuration = 3.0f;
        [SerializeField] private float _pursueSpeed = 5.0f;
        [SerializeField] private float _pursueStopDistance = 2.0f;
        [SerializeField] private float _changeStateDelay = 3.0f;
        [SerializeField] private float _visionRange = 20.0f;  -
        [SerializeField] private float _visionAngle = 80.0f;  -
        [SerializeField] private bool _haveRangeAttack = false;
        [SerializeField] private float _rangeAttackDistance = 50.0f;

        #endregion


        #region Properties

        public NpcType Type { get => _npcType; }

        public float MaxHealth { get => _maxHealth; }

        public float PatrolSpeed { get => _patrolSpeed; }

        public float PatrolStopDuration { get => _patrolStopDuration; }

        public float PursueSpeed { get => _pursueSpeed; }

        public float PursueStopDistance { get => _pursueStopDistance; }

        public float ChangeStateDelay { get => _changeStateDelay; }

        public float VisionRange { get => _visionRange; }

        public float VisionAngle { get => _visionAngle; }

        public bool HaveRangeAttack { get => _haveRangeAttack; }

        public float RangeAttackDistance { get => _rangeAttackDistance; }

        #endregion
        */


    }
}
