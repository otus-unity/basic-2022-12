using System;
using UnityEngine;

namespace Game.Scripts.Characters
{
    [Serializable]
    public class Weapon
    {
        [SerializeField]
        private WeaponType _weaponType;

        [SerializeField]
        private int _damage;

        public WeaponType Type => _weaponType;
        public int Damage => _damage;
    }
}