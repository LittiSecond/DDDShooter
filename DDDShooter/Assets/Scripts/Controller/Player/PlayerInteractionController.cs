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

        #endregion


        #region ClassLifeCycles

        public PlayerInteractionController()
        {
            _mask = LayerManager.GetLayerMask(LayerManager.Layer.Default, LayerManager.Layer.Ground,
                LayerManager.Layer.MoveableObjects, LayerManager.Layer.Interactable);
        }

        #endregion


        #region Methods
        
        public void Interact()
        {
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
                            Debug.Log("PlayerInteractionController->Interact: not realized interaction.");
                            break;
                    }
                }
                else
                {
                    _warningMessageText.Show(TextConstants.TOO_FAR_TEXT_ID);
                }
            }
        }

        private void SendMessageToUi(string message, bool shouldTranslate = true)
        {
            if (_messageUiText)
            {
                if (message != null)
                {
                    if (shouldTranslate)
                    {
                        message = LangManager.Instance.Text(
                            TextConstants.UI_GROUP_ID, message);
                    }
                    _messageUiText.Text = message;
                }
                else
                {
                    _messageUiText.ClearText();
                }
            }
        }

        //public override void On(params BaseObjectScene[] body)
        //{
        //    if (IsActive) return;
        //    if (body == null) return;
        //    if (body.Length == 0) return;
        //    PlayerBody pbody = body[0] as PlayerBody;
        //    if (!pbody) return;

        //    _head = pbody.Head;
        //    base.On(body);
        //}

        public void On(Transform head)
        {
            if (IsActive) return;
            if (head)
            {
                _head = head;
                base.On();
            }
        }

        public override void Off()
        {
            base.Off();
            _head = null;
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
                        SendMessageToUi(_interactableObject.GetMessageIfTarget(), false);
                    }
                    else
                    {
                        string message = LangManager.Instance.Text(TextConstants.UI_GROUP_ID, TextConstants.LOOK_AT_TEXT_ID);
                        SendMessageToUi(message + " " + collider.gameObject.name, false);
                    }
                }
                _distanceToTarget = hit.distance;
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
            _inventory = ServiceLocator.Resolve<Inventory>();
            _propertyController = ServiceLocator.Resolve<PlayerPropertyController>();
            _messageUiText = UiInterface.InteractMessageText;
            _warningMessageText = UiInterface.WarningMessageText;
            SendMessageToUi(null);
        }

        #endregion
    }
}
