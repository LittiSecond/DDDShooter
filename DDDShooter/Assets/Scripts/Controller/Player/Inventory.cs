using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class Inventory : BaseController //, IInitialization
    {
        #region Fields
 
        public const int WEAPON_SLOTS_QUANTITY = 5;

        private Weapon[] _weapons = new Weapon[WEAPON_SLOTS_QUANTITY];
        private Transform _head;

        #endregion


        #region Methods

        public Weapon GetWeapon(int index)
        {
            if (index < 0 || index >= _weapons.Length)
            {
                return null;
            }
            else
            {
                return _weapons[index];
            }
        }

        public SmallBattery GetSmallBattery()
        {
            // временно - батарейки в инвентаре бесконечны.
            return new SmallBattery();
        }

        public Clip GetClip(AmmunitionType ammunitionType)
        {
            // временно - запас обойм в инвентаре бесконечный.
            int quantity = 1;
            switch (ammunitionType)
            {
                case AmmunitionType.Bullet:
                    quantity = 30;
                    break;
                case AmmunitionType.Shell:
                    quantity = 3;
                    break;
                default:
                break;
            }
            
            return new Clip(ammunitionType, quantity);
        }

        public int PickUpWeapon(Weapon weapon)
        {
            int slotIndex = -1;
            if (weapon != null)
            {
                slotIndex = FindEmptySlotIndex();
                if (slotIndex >= 0)
                {
                    _weapons[slotIndex] = weapon;
                    weapon.DisablePhysics();
                    weapon.JoinTo(_head);
                    weapon.IsVisible = false;
                }
            }
            return slotIndex;
        }


        private int FindEmptySlotIndex()
        {
            int index = -1;
            for (int i = 0; i < _weapons.Length; i++)
            {
                if (_weapons[i] == null)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public void DropWeapon(int slotIndex)
        {
            if (slotIndex >= 0 && slotIndex < _weapons.Length)
            {
                Weapon weapon = _weapons[slotIndex];
                if ( weapon != null)
                {
                    _weapons[slotIndex] = null;
                    weapon.JoinTo(null);
                    weapon.IsVisible = true;
                    weapon.EnablePhysics();
                }
            }
        }

        public void On(Transform head)
        {
            if (IsActive) return;
            if (!head) return;
            
            _head = head;

            Weapon[] weapons = _head.GetComponentsInChildren<Weapon>();
            // Не нравится мне этот способ, надо бы сделать ... может класс, 
            //   который при старте построит и настроит персонажа? ...

            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].IsVisible = false;
                _weapons[i] = weapons[i];
                weapons[i].DisablePhysics();
            }

            base.On();
        }

        public override void Off()
        {                               // подумать, что тут надо делать с оружием
            base.Off();
            _head = null;
        }

        #endregion


        //#region IInitialization

        //public void Initialization()
        //{

        //}

        //#endregion
    }
}
