using UnityEngine;

using Geekbrains;


namespace DddShooter
{
    public sealed class PlayerController : BaseController, IExecute, IInitialization
    {

        #region Fields

        private GameObject _playerCharacterPrefab;
        //private Transform _cameraTransform;
        private Transform _head;
        private Transform _characterTransform;
        private Transform _bodyCentre;
        private PlayerBody _playerBody;
        private CharacterController _characterController;

        private TimeRemaining _timeRemaining;

        private readonly UnitMotor _motor;

        private Vector3 _playerRespawnPosition;
        private Quaternion _playerRespawnRotation;
        private float _cretePlayerCharacterDelay = 0.1f;
        private bool _isPlayerAlive;
        private bool _haveBodyInstance;

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
            //_cameraTransform = Camera.main.transform;
        }

        #endregion


        #region Methods

        public void SpawnPlayerCharacter()
        {
            if (IsActive && !_isPlayerAlive)
            {
                CreatePlayerCharacter();
            }
        }

        private void CreatePlayerCharacter()
        {
            if (!_isPlayerAlive && _playerCharacterPrefab)
            {
                if (_haveBodyInstance)
                {
                    _characterTransform.position = _playerRespawnPosition;
                    _characterTransform.rotation = _playerRespawnRotation;
                    _playerBody.Activate();
                }
                else
                {
                    InstantiateBody();
                }

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
                _playerRespawnRotation = go.transform.rotation;
            }
        }

        private void GetPrefab()
        {
            _playerCharacterPrefab = ResourcesManager.GetPrefab(PrefabId.PlayerCharacter);
        }

        private void ActivateOtherControllers()
        {
            FlashLightModel model = ServiceLocatorMonoBehaviour.GetService<FlashLightModel>();
            ServiceLocator.Resolve<FlashLightController>().On(model);

            PlayerHealth health = ServiceLocator.Resolve<PlayerHealth>();
            health.On(_playerBody);
            health.OnDeathEventHandler += DestroyPlayerCharacter;

            ServiceLocator.Resolve<PlayerInteractionController>().On(_head);

            ServiceLocator.Resolve<PlayerPropertyController>().On();
            //ServiceLocator.Resolve<PlayerPropertyController>().SelectNextWeapon();

            ServiceLocator.Resolve<PlayerSounds>().On(_playerBody);

            ServiceLocator.Resolve<Inventory>().On(_head);

            ServiceLocator.Resolve<MainCameraController>().Connect(_head);

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

        private void DestroyPlayerCharacter()
        {
            DeactivateOtherControllers();
            _isPlayerAlive = false;
            _playerBody.Deactivate();
            PlayerManager.PlayerDestroyed();
        }

        private void DeactivateOtherControllers()
        {
            ServiceLocator.Resolve<FlashLightController>().Off();
            ServiceLocator.Resolve<PlayerHealth>().Off();
            ServiceLocator.Resolve<PlayerInteractionController>().Off();
            ServiceLocator.Resolve<PlayerPropertyController>().SelectWeapon(-1);
            ServiceLocator.Resolve<PlayerPropertyController>().Off();
            ServiceLocator.Resolve<Inventory>().Off();
            ServiceLocator.Resolve<MainCameraController>().Disconnect();
            ServiceLocator.Resolve<PlayerSounds>().Off();
        }

        private void InstantiateBody()
        {
            GameObject go = Object.Instantiate(_playerCharacterPrefab, _playerRespawnPosition, _playerRespawnRotation);
            _characterTransform = go.transform;

            _playerBody = _characterTransform.GetComponent<PlayerBody>();
            _head = _playerBody.Head;
            _bodyCentre = _playerBody.BodyCentre;
            _characterController = _playerBody.CharacterController;

            _motor.SetObjectToMotor(_characterController, _head);

            _haveBodyInstance = true;
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
            FindRespawnPosition();
            GetPrefab();
            _timeRemaining = new TimeRemaining(CreatePlayerCharacter, _cretePlayerCharacterDelay);
            _timeRemaining.AddTimeRemaining();

            PauseController controller = ServiceLocator.Resolve<PauseController>();
            controller.SwichPauseEvent += SwithPause;
        }

        #endregion
    }
}
