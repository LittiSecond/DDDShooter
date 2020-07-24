using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    // объект для экспериментов. Не останется в релизе.
    public sealed class StrangeOrbModel : BaseObjectScene, IInteractable, IPickUpTool
    {
        private const string MESSAGE = "I am strange Orb.";

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
            return MESSAGE;
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
