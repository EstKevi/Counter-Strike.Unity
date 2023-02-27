using System.Collections;
using Script.Other;
using Script.player.PlayerBody.Hand;
using Script.player.PlayerBody.heal;
using Script.player.RespawnPlayer;
using Unity.Netcode;
using UnityEngine;

namespace Script.player.Death
{
    [RequireComponent(typeof(Player))]
    public class PlayerDeath : NetworkBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private Player player;
        [SerializeField] private Respawn respawn;
        [SerializeField] private HandWeapon handWeapon;

        private void Awake()
        {
            health.EnsureNotNull();
            player.EnsureNotNull();
            handWeapon.EnsureNotNull();
        }

        private void Start()
        {
            health.playerDeath.AddListener(() =>
            {
                if (IsOwner)
                {
                    Death();
                }
            });

            respawn.playerRespawned.AddListener(() =>
            {
                if (!IsOwner) return;
                health.ResetHealth();
                handWeapon.ResetWeapon();
            });
        }

        private void Death()
        {
            if (!IsOwner) return;
            player.RespawnPlayer();
        }
    }
}
