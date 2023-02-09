using UnityEngine;

namespace Script.player.Player.heal
{
    public class Heals : MonoBehaviour, IDamageable
    {
        [SerializeField] private int heal;
        public int Heal => heal;

        public void ApplyHeal(int regenHeal)
        {
            while (heal + regenHeal > 100)
            {
                --regenHeal;
            }

            heal += regenHeal;
        }
    
        public void ApplyDamage(int damage)
        {
            Debug.Log($"Damage: {damage}");
            heal -= damage;
            if (heal < 0)
                heal = 0;
        }
    }
}