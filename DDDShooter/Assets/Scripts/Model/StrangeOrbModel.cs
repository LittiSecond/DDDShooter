using UnityEngine;
using Geekbrains;
using DddShooter.Test;

namespace DddShooter
{
    // объект для экспериментов. Не останется в релизе.
    public sealed class StrangeOrbModel : BaseObjectScene, IInteractable, IPickUpTool
    {
        #region Fields

        [SerializeField] private OrbSettings _settingsScriptableObject;

        private string _message = "No message";
        private MeshRenderer _meshRenderer;

        #endregion


        #region Properties

        private Material ThisMaterial
        {
            set
            {
                if (_meshRenderer)
                {
                    _meshRenderer.material = value;
                }
            }
            get
            {
                if (_meshRenderer)
                {
                    return _meshRenderer.material;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion


        #region UnityMethods

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            OnValidate();
        }


        private void OnValidate()
        {
            if (_settingsScriptableObject)
            {
                ThisMaterial = _settingsScriptableObject.Material;
                _message = _settingsScriptableObject.Message;
            }
        }
        #endregion


        #region Methods



        #endregion


        #region IInteractable

        public bool IsTarget
        {
            set
            {
                //CustumDebug.Log("StrangeOrbModel->IsTarget = " + value.ToString());
            }
        }

        public string GetMessageIfTarget()
        {
            return _message;
        }

        public void Interact()
        {
            CustumDebug.Log("StrangeOrbModel->Interact: ");
        }

        public InteractType InteractType => InteractType.None;

        #endregion


        #region IPickUpTool

        public void DisablePhysics()
        {
                CustumDebug.Log("StrangeOrbModel->DisablePhysic: ");
        }

        public void EnablePhysics()
        {
            CustumDebug.Log("StrangeOrbModel->EnablePhysic: ");
        }

        #endregion
    }
}
