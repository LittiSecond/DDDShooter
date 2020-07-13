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
        private IInteractable _hittedTarget;

        private LayerMask _mask; 
        private float _lookRange = 20.0f;

        //private readonly string _message = "Look at: ";
        private const int LOOK_AT_TEXT_ID = 1;
        private const int CANNOT_PICK_UP_WEAPON_TEXT_ID = 6;

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
            if (_hittedTarget != null)
            {
                switch (_hittedTarget.InteractType)
                {
                    case InteractType.ExternalUse:
                    _hittedTarget.Interact();
                        break;
                    case InteractType.PickUpTool:
                        Weapon weapon = _hittedTarget as Weapon;
                        if (weapon != null)
                        {
                            int slotIdex = _inventory.PickUpWeapon(weapon);
                            if (slotIdex >= 0)
                            {
                                _propertyController.SelectWeapon(slotIdex);
                            }
                            else
                            {
                                _warningMessageText.Show(CANNOT_PICK_UP_WEAPON_TEXT_ID);
                            }
                        }
                        break;
                    default:
                        CustumDebug.Log("PlayerInteractionController->Interact: not realized interaction.");
                        break;
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
                        SendMessageToUi(TextConstants.GetText(LOOK_AT_TEXT_ID) + collider.gameObject.name);
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
