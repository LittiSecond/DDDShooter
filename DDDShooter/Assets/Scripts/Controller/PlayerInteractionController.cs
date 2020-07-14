using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class PlayerInteractionController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private UiInteractMessageText _messageUiText;
        private UiWarningMessageText _warningMessageText;
        private Transform _head;
        private Inventory _inventory;
        private PlayerPropertyController _propertyController;
        private Collider _hittedCollider;
        private IInteractable _interactableObject;

        private LayerMask _mask; 
        private float _lookRange = 20.0f;
        private float _interactRange = 3.0f;
        private float _distanceToTarget;

        //private readonly string _message = "Look at: ";
        private const int LOOK_AT_TEXT_ID = 1;
        private const int TOO_FAR_TEXT_ID = 8;

        #endregion


        #region ClassLifeCycles

        public PlayerInteractionController()
        {
            _mask = LayerManager.GetLayerMask(LayerManager.Layer.Default, LayerManager.Layer.Ground,
                LayerManager.Layer.MoveableObjects, LayerManager.Layer.Interactable);
        }

        #endregion


        #region Methods

        //public override void On()
        //{
        //    base.On();
        //}

        public void Interact()
        {
            //CustumDebug.Log("PlayerInteractionController->Interact: _distanceToTarget = " + 
            //    _distanceToTarget.ToString());
            if (_interactableObject != null)
            {
                if (_distanceToTarget <= _interactRange)
                {
                    switch (_interactableObject.InteractType)
                    {
                        case InteractType.ExternalUse:
                            _interactableObject.Interact();
                            break;
                        case InteractType.PickUpTool:
                            _propertyController.PickUpWeapon(_interactableObject as Weapon);
                            break;
                        default:
                            CustumDebug.Log("PlayerInteractionController->Interact: not realized interaction.");
                            break;
                    }
                }
                else
                {
                    _warningMessageText.Show(TOO_FAR_TEXT_ID);
                }
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
                    if (_interactableObject != null)
                    {
                        _interactableObject.IsTarget = false;
                        _interactableObject = null;
                    }

                    _hittedCollider = collider;
                    IInteractable target = collider.GetComponent<IInteractable>();
                    if (target != null)
                    {
                        _interactableObject = target;
                        _interactableObject.IsTarget = true;
                        SendMessageToUi(_interactableObject.GetMessageIfTarget());
                    }
                    else
                    {
                        SendMessageToUi(TextConstants.GetText(LOOK_AT_TEXT_ID) + collider.gameObject.name);
                    }

                    _distanceToTarget = hit.distance;
                    //Vector3 v = hit.point;
                }
            }
            else
            {
                if (_interactableObject != null)
                {
                    _interactableObject.IsTarget = false;
                    _interactableObject = null;
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
            On();
            _head = Camera.main.transform;
            _inventory = ServiceLocator.Resolve<Inventory>();
            _propertyController = ServiceLocator.Resolve<PlayerPropertyController>();
            _messageUiText = UiInterface.InteractMessageText;
            _warningMessageText = UiInterface.WarningMessageText;
            SendMessageToUi(null);
        }

        #endregion
    }
}
