using UnityEngine;

namespace Script.Bonus.BonusType
{
    public class HealthBonus : BonusBehaviour
    {
        [SerializeField] private int health;
        public int Health => health;
    }
}