using Script.Other;
using Script.player.Hand;
using Script.player.heal;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace Script.player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(NetworkTransform))]
    [RequireComponent(typeof(NetworkObject))]
    [RequireComponent(typeof(PlayerMove))]
    [RequireComponent(typeof(PlayerInfo))]
    [RequireComponent(typeof(HandWeapon))]
    [RequireComponent(typeof(Health))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private EntryPoint entryPoint;
        [SerializeField] private PlayerInfo playerInfo;

        private void Awake()
        {
            entryPoint = FindObjectOfType<EntryPoint>().EnsureNotNull();
            playerInfo.EnsureNotNull()
                .changesStatsEvent.AddListener(
                    (heal, ammo, stock) =>
                        entryPoint.ChangeStats(heal, ammo, stock)
                );
        }
    }
}