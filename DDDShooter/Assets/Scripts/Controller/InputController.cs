using UnityEngine;

namespace Geekbrains
{
    public sealed class InputController : BaseController, IExecute
    {
        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _cancel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
        private KeyCode _pause = KeyCode.Tab;
        private KeyCode _interact = KeyCode.E;
        private KeyCode _hideWeapon = KeyCode.Alpha0;
        //private KeyCode _weapon1 = KeyCode.Alpha1;
        //private KeyCode _weapon2 = KeyCode.Alpha2;
        //private KeyCode _weapon3 = KeyCode.Alpha3;
        //private KeyCode _weapon4 = KeyCode.Alpha4;
        //private KeyCode _weapon5 = KeyCode.Alpha5;
        private KeyCode _lastWeapon = KeyCode.Alpha5;
        private int _mouseButton = (int)MouseButton.LeftButton;
        private FlashLightModel _flashLightModel;
        
        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _flashLightModel = Object.FindObjectOfType<FlashLightModel>();
        }
        
        public void Execute()
        {
            if (!IsActive) return;
            if (Input.GetKeyDown(_activeFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch(_flashLightModel);
            }
            
            if (Input.GetKeyDown(_pause))
            {
                ServiceLocator.Resolve<PauseController>().SwithPause();
            }
            if (Input.GetKeyDown(_interact))
            {
                ServiceLocator.Resolve<PlayerInteractionController>().Interact();
            }
            if (Input.GetKeyDown(_reloadClip))
            {
                ServiceLocator.Resolve<FlashLightController>().ReplaceBattery();
            }
            if (Input.GetMouseButton(_mouseButton))
            {
                ServiceLocator.Resolve<WeaponController>().Fire();
            }

            CheckSwitchWeapon();

            if (Input.GetKeyDown(_cancel))
            {
                GameController gc = Object.FindObjectOfType<GameController>();
                if (gc)
                {
                    gc.ExitProgramm();
                }
            }


        }

        private void CheckSwitchWeapon()
        {
            int index = -1;
            KeyCode keyCode = _hideWeapon;
            while (keyCode <= _lastWeapon && index == -1)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    index = keyCode - _hideWeapon;
                }
                keyCode++;
            }
            if (index >= 0 )
            {
                ServiceLocator.Resolve<PlayerPropertyController>().SelectWeapon(index - 1);
            }
        }

    }
}
