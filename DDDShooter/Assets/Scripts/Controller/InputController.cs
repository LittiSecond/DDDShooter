using UnityEngine;

namespace Geekbrains
{
    public sealed class InputController : BaseController, IExecute
    {
        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _cancel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
        private KeyCode _pause = KeyCode.Tab;         //  <------- added
        private KeyCode _interact = KeyCode.E;        //  <------- added
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
            // -------------------------------- added
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
            if (Input.GetKeyDown(_cancel))
            {
                GameController gc = Object.FindObjectOfType<GameController>();
                if (gc)
                {
                    gc.ExitProgramm();
                }
            }
            // ------------------------------------

        }
    }
}
