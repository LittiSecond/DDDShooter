using System;
using UnityEngine;
using static UnityEngine.Random;

namespace Geekbrains
{
    public sealed class FlashLightModel : BaseObjectScene
    {
        [SerializeField] private float _speed = 11.0f;
        [SerializeField] private float _startBatteryChargeMax = 100.0f;// <-------- changed
        [SerializeField] private float _intensity = 1.5f;
        private SmallBattery _battery;                                 // <------ added
        private Light _light;
        private Transform _goFollow;
        private Vector3 _vecOffset;
        private float _share;
        private float _takeAwayTheIntensity;

        //public float Charge                                     // <--------- changed ( it do't used )
        //{                //=> BatteryChargeCurrent / _batteryChargeMax;  <--- was
        //    get
        //    {
        //        if (_battery != null)
        //        {
        //            return _battery.ChargeCurrent / _battery.ChargeMax;
        //        }
        //        else
        //        {
        //            return 0.0f;
        //        }
        //    }
        //}

        public float BatteryChargeCurrent                        // <------------ changed
        {                                 //get; private set; }     <---  was
            get
            {
                return (_battery != null) ? (_battery.ChargeCurrent) : 0.0f;
            }
            set
            {
                if (_battery != null)
                {
                    _battery.ChargeCurrent = value;
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();            
            _light = GetComponent<Light>();
            _goFollow = Camera.main.transform;
            _vecOffset = transform.position - _goFollow.position;
            //BatteryChargeCurrent = _batteryChargeMax;                        // <--- changed
            _light.intensity = _intensity;
                                                          
            
            ReplaceBattery( new SmallBattery(_startBatteryChargeMax));           // <--- added
            
            //_share = _battery.ChargeMax / 4.0f;                                // <--- changed
            //_takeAwayTheIntensity = _intensity / (_battery.ChargeMax * 100.0f);// <--- changed
        }

        public void Switch(FlashLightActiveType value)
        {
            switch (value)
            {
                case FlashLightActiveType.On:
                    _light.enabled = true;
                    transform.position = _goFollow.position + _vecOffset;
                    transform.rotation = _goFollow.rotation;
                    break;
                case FlashLightActiveType.None:
                    break;
                case FlashLightActiveType.Off:
                    _light.enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public void Rotation()
        {
            transform.position = _goFollow.position + _vecOffset;
            transform.rotation = Quaternion.Lerp(transform.rotation,
                _goFollow.rotation, _speed * Time.deltaTime);
        }
        
        public bool EditBatteryCharge()
        {
            if (BatteryChargeCurrent > 0)
            {
                BatteryChargeCurrent -= Time.deltaTime;

                if (BatteryChargeCurrent < _share)
                {
                    _light.enabled = Range(0, 100) >= Range(0, 10);
                }
                else
                {
                    _light.intensity -= _takeAwayTheIntensity;
                }
                return true;
            }
            
            return false;
        }

        public bool LowBattery()
        {
            return BatteryChargeCurrent <= _battery.ChargeMax / 2.0f;  // <--- changed
        }

        public bool BatteryRecharge()
        {
            if (BatteryChargeCurrent < _battery.ChargeMax)         // <--- changed
            {
                BatteryChargeCurrent += Time.deltaTime;
                return true;
            }
            return false;
        }

        public SmallBattery ReplaceBattery(SmallBattery newBattery) //  <--- added
        {
            SmallBattery oldBattery = _battery;
            _battery = newBattery;
            if (_battery != null)
            {
                _light.intensity = _intensity;
                _share = _battery.ChargeMax / 4.0f;
                _takeAwayTheIntensity = _intensity / (_battery.ChargeMax * 100.0f);
            }
            return oldBattery;
        }
    }
}
