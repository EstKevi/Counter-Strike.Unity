using System;
using Script.player.heal;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace Script.player.PlayerBody.heal
{
    public class Health : NetworkBehaviour, IDamageable
    {
        [SerializeField] private NetworkVariable<int> heal = new(
            100,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

        private int firstValueHealth;
        public UnityEvent playerDeath = new();

        private void Awake() => firstValueHealth = heal.Value;

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
        
        public void ApplyDamage(int damage)
        {
            if (!IsOwner) return;
            heal.Value -= damage;
            heal.Value =  Mathf.Clamp(heal.Value, 0, firstValueHealth);
            if(heal.Value <= 0) playerDeath.Invoke();
        }

        private void OnDisable()
        {
            if (IsOwner)
            {
                heal.Value = firstValueHealth;
            }
        }
    }
}