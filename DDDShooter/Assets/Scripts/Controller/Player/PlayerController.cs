using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    public sealed class PlayerController : BaseController, IExecute, IInitialization
    {

        #region Fields

        private GameObject _playerCharacterPrefab;
        private Transform _cameraTransform;
        private Transform _head;
        private Transform _characterTransform;
        private Transform _bodyCentre;
        private PlayerBody _playerBody;
        private TimeRemaining _timeRemaining;

        private readonly UnitMotor _motor;

        private Vector3 _playerRespawnPosition;
        private float _cretePlayerCharacterDelay = 0.1f;
        private bool _isPlayerAlive;

        #endregion


        #region Properties

        public Transform CharacterTransform
        {
            get => _characterTransform;
        }

        #endregion


        #region ClassLifeCycles

        public PlayerController()
        {
            _motor = new UnitMotor();
            _cameraTransform = Camera.main.transform;
        }

        #endregion


        #region Methods

        private void CreatePlayerCharacter()
        {
            if (!_isPlayerAlive && _playerCharacterPrefab)
            {
                GameObject go = Object.Instantiate(_playerCharacterPrefab, _playerRespawnPosition, Quaternion.identity);
                _characterTransform = go.transform;

                _playerBody = _characterTransform.GetComponent<PlayerBody>();                
                _head = _playerBody.Head;
                _bodyCentre = _playerBody.BodyCentre;
                _playerBody.FixCamera(_cameraTransform);

                CharacterController cc = go.GetComponent<CharacterController>();
                _motor.SetObjectToMotor(cc, _head);

                ServiceLocator.Resolve<MiniMapController>().SetPlayer(_characterTransform);

                _isPlayerAlive = true;

                ActivateOtherControllers();

                PlayerManager.SetPlayerTransform(_bodyCentre);
            }
        }

        private void FindRespawnPosition()
        {
            GameObject go = GameObject.FindGameObjectWithTag(TagManager.PLAYER_RESPAWN);
            if (go)
            {
                _playerRespawnPosition = go.transform.position;
            }
        }

        private void GetPrefab()
        {
            _playerCharacterPrefab = PrefabManager.GetPrefab(PrefabId.PlayerCharacter);
        }

        private void ActivateOtherControllers()
        {
            FlashLightModel model = ServiceLocatorMonoBehaviour.GetService<FlashLightModel>();
            ServiceLocator.Resolve<FlashLightController>().On(model);

            ServiceLocator.Resolve<PlayerHealth>().On(_playerBody);

            //ServiceLocator.Resolve<PlayerInteractionController>().On(_playerBody);            
            ServiceLocator.Resolve<PlayerInteractionController>().On(_head);

            ServiceLocator.Resolve<PlayerPropertyController>().On();

            ServiceLocator.Resolve<Inventory>().On(_head);
        }

        private void SwithPause(bool isPause)
        {
            if (isPause)
            {
                base.Off();
            }
            else
            {
                base.On(null);
            }
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;
            if (_isPlayerAlive)
            {
                _motor.Move();
            }
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            //Transform transform = ServiceLocatorMonoBehaviour.GetService<PlayerBody>().Transform;
            //ServiceLocator.Resolve<MiniMapController>().SetPlayer(transform);
            FindRespawnPosition();
            GetPrefab();
            _timeRemaining = new TimeRemaining(CreatePlayerCharacter, _cretePlayerCharacterDelay);
            _timeRemaining.AddTimeRemaining();
            //CreatePlayerCharacter();

            PauseController controller = ServiceLocator.Resolve<PauseController>();
            controller.SwichPauseEvent += SwithPause;
        }

        #endregion
    }
}
