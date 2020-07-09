using UnityEngine;

namespace Geekbrains
{
    public sealed class FlashLightController : BaseController, IExecute, IInitialization
    {
        private FlashLightModel _flashLightModel;
        private bool _isPauseInGame;                        //<----------- added

        public void Initialization()
        {
            UiInterface.LightUiText.SetActive(false);
            ServiceLocator.Resolve<PauseController>().SwichPauseEvent += SwichPause;   //<------- added
        }

        public override void On(params BaseObjectScene[] flashLight)
        {
            if (IsActive) return;
            if (flashLight.Length > 0) _flashLightModel = flashLight[0] as FlashLightModel;
            if (_flashLightModel == null) return;
            if (_flashLightModel.BatteryChargeCurrent <= 0) return;
            base.On(_flashLightModel);
            _flashLightModel.Switch(FlashLightActiveType.On);
            UiInterface.LightUiText.SetActive(true);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _flashLightModel.Switch(FlashLightActiveType.Off);;
            UiInterface.LightUiText.SetActive(false);
        }

        public void Execute()
        {
            if(!IsActive)
            {
                return;
            }
            if (!_isPauseInGame)              // <---- added  
            {
                if (_flashLightModel.EditBatteryCharge())
                {
                    UiInterface.LightUiText.Text = _flashLightModel.BatteryChargeCurrent;
                    _flashLightModel.Rotation();
                }
                else
                {
                    Off();
                }
            }
        }

        public void ReplaceBattery()      //  <----------------- added
        {
            //  По идее, тут надо делать так:
            //  - проверить, есть ли в инвентаре батарея;
            //  - если есть, то взять её из инвентаря;
            //  - вставить батарею в фонарик, при этом получаем из него старую батарею;
            //  - положить старую батарею в инвентарь. 
            //  Потом старую батарею можно будет где-нибудь зарядить, выкинуть или разобрать на ресурсы для крафта
            //
            //  Но сейчас инвентаря нет.
            //  
            if (_flashLightModel)
            {
                Off();
                SmallBattery battery = _flashLightModel.ReplaceBattery(null); // вынули батарею
                if (battery != null)
                {
                    battery.ChargeCurrent = battery.ChargeMax;                    // как бы зарядили
                }
                else
                {
                    battery = new SmallBattery();
                }
                _flashLightModel.ReplaceBattery(battery);                     // вставили обратно
            }
        }

        public void SwichPause(bool isPause)
        {
            _isPauseInGame = isPause;
        }
    }
}
