using UnityEngine;


namespace Geekbrains
{
    public sealed class Controllers : IInitialization
    {
        private readonly IExecute[] _executeControllers;

        public int Length => _executeControllers.Length;

        public Controllers()
        {
            IMotor motor = new UnitMotor(ServiceLocatorMonoBehaviour.GetService<CharacterController>());
            ServiceLocator.SetService(new TimeRemainingController());
            ServiceLocator.SetService(new PlayerController(motor));
            ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new InputController());
            ServiceLocator.SetService(new PlayerInteractionController());
            ServiceLocator.SetService(new PauseController());
            ServiceLocator.SetService(new WeaponController());
            ServiceLocator.SetService(new Inventory());
            ServiceLocator.SetService(new PlayerPropertyController());

            _executeControllers = new IExecute[5];

            _executeControllers[0] = ServiceLocator.Resolve<TimeRemainingController>();
            
            _executeControllers[1] = ServiceLocator.Resolve<PlayerController>();

            _executeControllers[2] = ServiceLocator.Resolve<FlashLightController>();

            _executeControllers[3] = ServiceLocator.Resolve<InputController>();

            _executeControllers[4] = ServiceLocator.Resolve<PlayerInteractionController>();
        }
        
        public IExecute this[int index] => _executeControllers[index];

        public void Initialization()
        {
            foreach (var controller in _executeControllers)
            {
                if (controller is IInitialization initialization)
                {
                    initialization.Initialization();
                }
            }
            
            ServiceLocator.Resolve<InputController>().On();
            ServiceLocator.Resolve<PlayerController>().On();
            ServiceLocator.Resolve<PauseController>().On();
            ServiceLocator.Resolve<Inventory>().Initialization();
            ServiceLocator.Resolve<PlayerPropertyController>().Initialization();

        }
    }
}
