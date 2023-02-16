using Script.Bonus.BonusCore;
using Script.player.Hand;
using UnityEngine;

namespace Script.Bonus.BonusType
{
    public class AmmoBonus : BonusBehaviour
    {
        [SerializeField] private int ammo;
        
        public override bool ApplyBonus(GameObject obj)
        {
            return obj.TryGetComponent<HandWeapon>(out var handPlayer) && handPlayer.ApplyStockBonus(ammo);
        }
    }
}