using UnityEngine;

namespace Script.weapon
{
    public interface IGun
    {
        void Shoot(Collider _);
        void Reload();
        void ResetWeapon();

        public int Ammo { get; }
        public int Stock { get; set; }
    }
}