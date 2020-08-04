using UnityEngine;
using DddShooter;


namespace Geekbrains
{
    public sealed class Controllers : IInitialization
    {
        private readonly IExecute[] _executeControllers;

        public int Length => _executeControllers.Length;

        public Controllers()
        {
            //IMotor motor = new UnitMotor(ServiceLocatorMonoBehaviour.GetService<CharacterController>());
            ServiceLocator.SetService(new TimeRemainingController());
            ServiceLocator.SetService(new MiniMapController());
            ServiceLocator.SetService(new PlayerController());
            ServiceLocator.SetService(new PlayerHealth());
            ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new InputController());
            ServiceLocator.SetService(new PlayerInteractionController());
            ServiceLocator.SetService(new PauseController());
            ServiceLocator.SetService(new WeaponController());
            ServiceLocator.SetService(new Inventory());
            ServiceLocator.SetService(new PlayerPropertyController());
            ServiceLocator.SetService(new NpcCommander());
            ServiceLocator.SetService(new MainCameraController());
            ServiceLocator.SetService(new GameOverLogick());


            _executeControllers = new IExecute[8];

            _executeControllers[0] = ServiceLocator.Resolve<TimeRemainingController>();

            _executeControllers[1] = ServiceLocator.Resolve<MiniMapController>();    
            
            _executeControllers[2] = ServiceLocator.Resolve<PlayerController>();

            _executeControllers[3] = ServiceLocator.Resolve<FlashLightController>();

            _executeControllers[4] = ServiceLocator.Resolve<InputController>();

            _executeControllers[5] = ServiceLocator.Resolve<PlayerInteractionController>();

            _executeControllers[6] = ServiceLocator.Resolve<NpcCommander>();

            _executeControllers[7] = ServiceLocator.Resolve<MainCameraController>();

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
            //ServiceLocator.Resolve<Inventory>().Initialization();
            ServiceLocator.Resolve<PlayerHealth>().Initialization();
            //ServiceLocator.Resolve<PlayerHealth>().On();
            ServiceLocator.Resolve<PlayerPropertyController>().Initialization();
            ServiceLocator.Resolve<NpcCommander>().On();
            ServiceLocator.Resolve<GameOverLogick>().Initialization();

        }
    }
}
