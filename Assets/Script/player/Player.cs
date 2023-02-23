using System;
using System.Collections;
using System.Collections.Generic;
using Script.player.PlayerBody.camera;
using Script.player.PlayerBody.Hand;
using Script.player.PlayerBody.heal;
using Unity.Netcode.Components;
using Unity.Netcode;
using Script.Other;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] private WeaponDictionary weaponDictionary;

        public UnityEvent moveEvent = new();

        private NetworkVariable<int> weaponId = new(
            0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

        private void Awake()
        {
            weaponDictionary = FindObjectOfType<WeaponDictionary>().EnsureNotNull();
            entryPoint = FindObjectOfType<EntryPoint>().EnsureNotNull();
            handWeapon.EnsureNotNull();
            playerCamera.EnsureNotNull();
            playerMove.EnsureNotNull();

            playerInfo.EnsureNotNull().changesStatsEvent.AddListener((heal, ammo, stock) =>
                entryPoint.ChangeStats(heal, ammo, stock)
            );
        }

        private void Start()
        {
            entryPoint.weaponId?.AddListener(id =>
                {
                    if(IsOwner)
                    {
                        weaponId.Value = id;
                        StartCoroutine(GrabWeaponAsync());
                        moveEvent.Invoke();
                    }
                }
            );
        }

        private IEnumerator GrabWeaponAsync()
        {
            if (!IsOwner) yield return null;
            yield return null;
            GrabWeaponServerRpc();
        }

        [ServerRpc]
        private void GrabWeaponServerRpc()
        {
            handWeapon.Grab(weaponDictionary.GetWeapon(weaponId.Value));
            GrabWeaponClientRpc();
        }
        
        [ClientRpc] private void GrabWeaponClientRpc() => handWeapon.Grab(weaponDictionary.GetWeapon(weaponId.Value));
        
    }
}