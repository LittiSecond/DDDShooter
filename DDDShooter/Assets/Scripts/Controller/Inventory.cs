using UnityEngine;
using Geekbrains;


namespace DddShooter
{
    public sealed class Inventory : IInitialization
    {
        #region Fields

        public const int WEAPON_SLOTS_QUANTITY = 5;

        private Weapon[] _weapons = new Weapon[WEAPON_SLOTS_QUANTITY];

        #endregion


        #region Methods

        public void Initialization()
        {
            _weapons = ServiceLocatorMonoBehaviour.GetService<CharacterController>().
                GetComponentsInChildren<Weapon>();   
                        // Не нравится мне этот способ, надо бы сделать ... может класс, 
                        //   который при старте построит и настроит персонажа? ...

            foreach (var weapon in _weapons)
            {
                weapon.IsVisible = false;
            }

        }

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


        #endregion
    }
}
