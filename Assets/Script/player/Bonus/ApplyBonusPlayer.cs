using Script.Bonus;
using Script.Bonus.BonusType;
using Script.player.Hand;
using Script.player.heal;
using Unity.Netcode;
using UnityEngine;

namespace Script.player.Bonus
{
    public class ApplyBonusPlayer : NetworkBehaviour, IApplyBonus
    {
        [SerializeField] private Health health;
        [SerializeField] private HandWeapon handWeapon;

        public bool ApplyBonus(BonusBehaviour bonusBehaviour)
        {
            if (!IsOwner) return false;

            if (bonusBehaviour.TryGetComponent<HealthBonus>(out var healthBonus))
            {
                ApplyBonusHealth(healthBonus.Health);
                return true;
            }

            if (bonusBehaviour.TryGetComponent<AmmoBonus>(out var ammoBonus))
            {
                return ApplyBonusStock(ammoBonus.Ammo);
            }

            return false;
        }
        private void ApplyBonusHealth(int bonusHealth) => health.Heal += bonusHealth;
        private bool ApplyBonusStock(int bonusStock) => handWeapon.ApplyStockBonus(bonusStock);
    }
}