using UnityEngine;

namespace Script.Bonus
{
    public class AmmoBonus : BonusBehaviour
    {
        [SerializeField] private int ammo;
        public int Ammo => ammo;
    }
}