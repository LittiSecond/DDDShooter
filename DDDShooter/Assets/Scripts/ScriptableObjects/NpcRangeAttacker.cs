using UnityEngine;


namespace DddShooter
{
    [CreateAssetMenu(fileName = "npcRangeAttacker", menuName =
    "CreateScriptableObject/NpcRangeAttacker", order = 1)]
    public class NpcRangeAttacker : NpcSettings
    {
        public float PursueSpeed = 5.0f;
        public float PursueStopDistance = 10.0f;
        public float RangeAttackDistance = 50.0f;
    }
}
