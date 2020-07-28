using UnityEngine;


namespace DddShooter
{
    [RequireComponent(typeof(BaseMineDetonateEffect))]
    public class MineModel : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _colorIndicator;
        private BaseMineDetonateEffect _detonateEffect;

        #endregion


        #region UnityMethods

        protected virtual void Awake()
        {

            _detonateEffect = GetComponent<BaseMineDetonateEffect>();

            //_colorIndicator = transform.Find("TypeMineIndicatorObject").gameObject;

            if (_colorIndicator != null)
            {
                Material tempMaterial = _colorIndicator.GetComponent<Renderer>().material;
                if (tempMaterial != null) tempMaterial.color = _detonateEffect.IndikationColor;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_detonateEffect.DetonateAction(other))
            {
                Destroy(gameObject);
            }
        }

        #endregion

    }
}
