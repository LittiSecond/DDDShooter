using UnityEngine;

namespace DddShooter
{
    public class MineDetonateEffectDamage : BaseMineDetonateEffect
    {

        [SerializeField, Range(0, 1000)] private float _baseDamage = 48;


        public override Color IndikationColor
        {
            get { return Color.red; }
        }


        public override bool DetonateAction(Collider other)
        {
            ITakerDamage takerDamag = other.gameObject.GetComponent<ITakerDamage>();
            if (takerDamag != null)
            {
                takerDamag.TakeDamage(_baseDamage);
                return true;
            }
            return false;
        }

    }
}