using System;
using Script.player.PlayerBody.camera;
using Script.player.PlayerBody.Hand;
using Script.player.PlayerBody.heal;
using Unity.Netcode.Components;
using Unity.Netcode;
using Script.Other;
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
    public class Player : NetworkBehaviour
    {
        [SerializeField] private EntryPoint entryPoint;
        [SerializeField] private PlayerInfo playerInfo;
        [SerializeField] private PlayerCamera playerCamera;
        [SerializeField] private HandWeapon handWeapon;
        [SerializeField] private PlayerMove playerMove;

        private void Awake()
        {
            entryPoint = FindObjectOfType<EntryPoint>().EnsureNotNull();
            handWeapon.EnsureNotNull();
            playerCamera.EnsureNotNull();
            playerMove.EnsureNotNull();

            playerInfo.EnsureNotNull().changesStatsEvent.AddListener((heal, ammo, stock) =>
                entryPoint.ChangeStats(heal, ammo, stock)
            );
        }
    }
}