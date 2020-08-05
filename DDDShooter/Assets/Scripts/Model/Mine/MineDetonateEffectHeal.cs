using UnityEngine;

namespace DddShooter
{
    public class MineDetonateEffectHeal : BaseMineDetonateEffect
    {
        [SerializeField, Range(0, 1000)] private float _baseHeal = 37;


        public override Color IndikationColor
        {
            get { return Color.green; }
        }


        public override bool DetonateAction(Collider other)
        {
            ITakerHealing tempISH = other.gameObject.GetComponent<ITakerHealing>();
            if (tempISH != null)
            {
                tempISH.TakeHealing(_baseHeal);
                return true;
            }
            return false;
        }


    }
}
