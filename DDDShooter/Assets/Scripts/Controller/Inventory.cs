﻿using UnityEngine;
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

        #endregion
    }
}
