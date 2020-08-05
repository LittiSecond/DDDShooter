using UnityEngine;


namespace DddShooter
{
    public abstract class BaseMineDetonateEffect : MonoBehaviour
    {
        public virtual Color IndikationColor
        {
            get { return Color.white; }
        }

        public abstract bool DetonateAction(Collider other);
    }
}
