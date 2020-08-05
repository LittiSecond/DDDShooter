using Geekbrains;


namespace DddShooter
{
    public sealed class FlashLightController : BaseController, IExecute, IInitialization
    {
        #region Properties

        private FlashLightModel _flashLightModel;

        private FlashLightActiveType _currentLightState;
        private bool _isPauseInGame;

        #endregion


        #region Methods

        public override void On(params BaseObjectScene[] flashLight)
        {
            if (IsActive) return;
            if (flashLight.Length > 0) _flashLightModel = flashLight[0] as FlashLightModel;
            if (_flashLightModel == null) return;
            if (_flashLightModel.BatteryChargeCurrent <= 0) return;
            base.On(_flashLightModel);
        }

        public override void Off()
        {
            if (!IsActive) return;
            LightOff();
            base.Off();
        }

        public SmallBattery ReplaceBattery(SmallBattery smallBattery)
        {
            SmallBattery oldBattery = null;
            if (IsActive)
            {
                if (_flashLightModel)
                {
                    LightOff();
                    oldBattery = _flashLightModel.ReplaceBattery(smallBattery);   // вставили обратно
                }
            }
            return oldBattery;
        }

        public void SwichPause(bool isPause)
        {
            _isPauseInGame = isPause;
        }


        public void SwitchLight()
        {
            if (IsActive)
            {
                if (_currentLightState == FlashLightActiveType.On)
                {
                    LightOff();
                }
                else  // _lightActiveType =  .Off или .None
                {
                    LightOn();
                }
            }
        }

        public void LightOn()
        {
            if (IsActive)
            {
                if (_currentLightState != FlashLightActiveType.On)
                {
                    _flashLightModel.Switch(FlashLightActiveType.On);
                    _currentLightState = FlashLightActiveType.On;
                    UiInterface.LightUiText.SetActive(true);
                }
            }
        }

        public void LightOff()
        {
            if (IsActive)
            {
                if (_currentLightState != FlashLightActiveType.Off)
                {
                    _flashLightModel.Switch(FlashLightActiveType.Off);
                    _currentLightState = FlashLightActiveType.Off;
                    UiInterface.LightUiText.SetActive(false);
                }
            }
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            UiInterface.LightUiText.SetActive(false);
            ServiceLocator.Resolve<PauseController>().SwichPauseEvent += SwichPause;
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }
            if (_currentLightState == FlashLightActiveType.On)
            {
                if (!_isPauseInGame)
                {
                    if (_flashLightModel.EditBatteryCharge())
                    {
                        UiInterface.LightUiText.Text = _flashLightModel.BatteryChargeCurrent;
                        _flashLightModel.Rotation();
                    }
                    else
                    {
                        LightOff();
                    }
                }
            }
        }

        #endregion
    }
}
