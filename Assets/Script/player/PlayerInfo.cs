using Script.Other;
using Script.player.heal;
using Script.player.PlayerBody.Hand;
using Script.player.PlayerBody.heal;
using UniRx;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace Script.player
{
    public class PlayerInfo : NetworkBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private HandWeapon hand;
        [SerializeField] private ReactiveProperty<int>[] reactiveProperties = new ReactiveProperty<int>[3];
        public UnityEvent<int, int, int> changesStatsEvent = new();

        private void Awake()
        {
            health.EnsureNotNull();
            hand.EnsureNotNull();
        }

        private void Start()
        {
            if(!IsOwner) return;
            foreach (var reactive in reactiveProperties)
            {
                reactive.Subscribe(_ =>
                    changesStatsEvent.Invoke(
                        health.Heal,
                        hand.WeaponAmmo,
                        hand.WeaponStock
                    )
                );
            }
        }

        private void Update()
        {
            if(!IsOwner) return;
            
            reactiveProperties[0].Value = hand.WeaponAmmo;
            reactiveProperties[1].Value = health.Heal;
            reactiveProperties[2].Value = hand.WeaponStock;
        }
    }
}