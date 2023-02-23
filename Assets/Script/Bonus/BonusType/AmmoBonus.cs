using Script.Bonus.BonusCore;
using Script.player.PlayerBody.Hand;
using UnityEngine;

namespace Script.Bonus.BonusType
{
    public class AmmoBonus : BonusBehaviour
    {
        [SerializeField] private int ammo;
        public override bool ApplyBonus(GameObject obj) => obj.TryGetComponent<HandWeapon>(out var handPlayer) && handPlayer.ApplyStockBonus(ammo);
    }
}