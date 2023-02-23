using Script.Bonus.BonusCore;
using Script.player.heal;
using Script.player.PlayerBody.heal;
using UnityEngine;

namespace Script.Bonus.BonusType
{
    public class HealthBonus : BonusBehaviour
    {
        [SerializeField] private int health;

        public override bool ApplyBonus(GameObject obj)
        {
            if (!obj.TryGetComponent<Health>(out var healthPlayer)) return false;
            healthPlayer.Heal += health;
            return true;
        }
    }
}