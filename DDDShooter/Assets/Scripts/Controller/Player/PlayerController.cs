using UnityEngine;

using DddShooter;


namespace Geekbrains
{
    public sealed class PlayerController : BaseController, IExecute, IInitialization
    {
        private readonly IMotor _motor;

        public PlayerController(IMotor motor)
        {
            _motor = motor;
        }

        public void Execute()
        {
            if (!IsActive) return;
            _motor.Move();
        }

        public void Initialization()
        {
            Transform transform = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform;
            ServiceLocator.Resolve<MiniMapController>().SetPlayer(transform);
        }
    }
}
