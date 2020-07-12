using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class PlayerInteractionController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private UiInteractMessageText _messageUiText;
        private Transform _head;
        private Collider _hittedCollider;
        private IInteractable _hittedTarget;

        private LayerMask _mask; 
        private float _lookRange = 20.0f;

        private readonly string _message = "Look at: ";

        #endregion


        #region ClassLifeCycles

        public PlayerInteractionController()
        {
            _mask = LayerManager.GetLayerMask(LayerManager.Layer.Default, LayerManager.Layer.Ground,
                LayerManager.Layer.MoveableObjects, LayerManager.Layer.Interactable);
        }

        #endregion


        #region Methods

        public override void On()
        {
            base.On();
            _messageUiText = UiInterface.InteractMessageText;
            SendMessageToUi(null);
        }

        public void Interact()
        {
            if (_hittedTarget != null)
            {
                _hittedTarget.Interact();
            }
        }

        private void SendMessageToUi(string message)
        {
            if (_messageUiText)
            {
                if (message != null)
                {
                    _messageUiText.Text = message;
                }
                else
                {
                    _messageUiText.Text = string.Empty;
                }
            }
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            //CustumDebug.Log("PlayerInteractionController->Execute:");
            if (!IsActive)
            {
                return;
            }

            RaycastHit hit;
            if ( Physics.Raycast(_head.position, _head.TransformDirection(Vector3.forward), out hit, _lookRange, _mask))
            {
                Collider collider = hit.collider;
                if ( collider != _hittedCollider)
                {
                    if (_hittedTarget != null)
                    {
                        _hittedTarget.IsTarget = false;
                        _hittedTarget = null;
                    }

                    _hittedCollider = collider;
                    IInteractable target = collider.GetComponent<IInteractable>();
                    if (target != null)
                    {
                        _hittedTarget = target;
                        _hittedTarget.IsTarget = true;
                        SendMessageToUi(_hittedTarget.GetMessageIfTarget());
                    }
                    else
                    {
                        SendMessageToUi(_message + collider.gameObject.name);
                    }
                }
            }
            else
            {
                if (_hittedTarget != null)
                {
                    _hittedTarget.IsTarget = false;
                    _hittedTarget = null;
                }

                if (_hittedCollider)
                {
                    SendMessageToUi(null);
                    _hittedCollider = null;
                }
            }
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            _head = Camera.main.transform;
            On();
        }

        #endregion
    }
}
