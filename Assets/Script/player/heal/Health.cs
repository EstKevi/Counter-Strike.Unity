using System;
using Unity.Netcode;
using UnityEngine;

namespace Script.player.heal
{
    public class Health : NetworkBehaviour, IDamageable
    {
        [SerializeField] private NetworkVariable<int> heal = new(
            100,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

        public int Heal
        {
            get => heal.Value;
            set
            {
                if (heal.Value >= 100) return;
                
                while (heal.Value + value > 100)
                    --value;
                heal.Value += value;
            }
        }
        
        public void ApplyDamage(int dmg)
        {
            if (!IsOwner) return;

            heal.Value -= dmg;
            if (heal.Value < 0)
                heal.Value = 0;
        }
    }
}