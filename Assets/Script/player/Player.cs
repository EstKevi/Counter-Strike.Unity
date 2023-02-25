using Script.player.Inputs.Keyboard;
using Script.player.PlayerBody.Hand;
using Script.player.PlayerBody.heal;
using Unity.Netcode.Components;
using Script.player.Menu;
using System.Collections;
using Script.weapon;
using Unity.Netcode;
using Script.Other;
using UnityEngine;

namespace Script.player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(NetworkTransform))]
    [RequireComponent(typeof(NetworkObject))]
    [RequireComponent(typeof(PlayerMenu))]
    [RequireComponent(typeof(PlayerMove))]
    [RequireComponent(typeof(PlayerInfo))]
    [RequireComponent(typeof(HandWeapon))]
    [RequireComponent(typeof(Health))]
    public class Player : NetworkBehaviour
    {
        [SerializeField] private EntryPoint entryPoint;
        [SerializeField] private PlayerInfo playerInfo;
        [SerializeField] private HandWeapon handWeapon;
        [SerializeField] private PlayerMenu playerMenu;
        
        private WeaponDictionary weaponDictionary;
        private IInput input = new PlugInput();
        private NetworkVariable<int> weaponId = new(
            0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

        private void Awake()
        {
            playerMenu.EnsureNotNull();
            entryPoint = FindObjectOfType<EntryPoint>().EnsureNotNull();
            handWeapon.EnsureNotNull();
            playerInfo.EnsureNotNull().changesStatsEvent.AddListener(
                (heal, ammo, stock) =>
                    entryPoint.ChangeStats(heal, ammo, stock)
            );
        }

        private void Start()
        {
            entryPoint.chooseWeapon?.AddListener(id =>
                {
                    if (IsOwner)
                    {
                        weaponId.Value = id;
                        StartCoroutine(GrabWeaponAsync());
                        playerMenu.Menu(true);
                        input = new KeyBoardInput();;
                    }
                }
            );
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            weaponDictionary = FindObjectOfType<WeaponDictionary>().EnsureNotNull();
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

        [ClientRpc]
        private void GrabWeaponClientRpc() => handWeapon.Grab(weaponDictionary.GetWeapon(weaponId.Value));
        
        private void Update()
        {
            if (IsOwner && input.KeyEscape())
            {
                playerMenu.Menu();;
                entryPoint.menuEvent.Invoke();
            }
        }
    }
}