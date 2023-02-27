using Script.player.Inputs.Keyboard;
using Script.player.PlayerBody.Hand;
using Script.player.PlayerBody.heal;
using Unity.Netcode.Components;
using System.Collections;
using Script.weapon;
using Unity.Netcode;
using Script.Other;
using Script.player.PlayerMove;
using Script.player.RespawnPlayer;
using Script.SpawnPoint;
using UnityEngine;

namespace Script.player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(NetworkTransform))]
    [RequireComponent(typeof(NetworkObject))]
    [RequireComponent(typeof(PlayerMoveSettings))]
    [RequireComponent(typeof(PlayerStatistics))]
    [RequireComponent(typeof(HandWeapon))]
    [RequireComponent(typeof(Health))]
    public class Player : NetworkBehaviour
    {
        [SerializeField] private EntryPoint entryPoint;
        [SerializeField] private PlayerStatistics playerStatistics;
        [SerializeField] private HandWeapon handWeapon;
        [SerializeField] private PlayerMoveSettings playerMoveSettings;
        [SerializeField] private Respawn respawn;
        [SerializeField] private Spawn spawn;
        
        private const float WaitForGiveWeapon = 0.1f;
        
        private WeaponDictionary weaponDictionary;
        private IInput input = new PlugInput();
        private NetworkVariable<int> weaponId = new(
            1,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

        private void Awake()
        {
            weaponDictionary = FindObjectOfType<WeaponDictionary>().EnsureNotNull();
            entryPoint = FindObjectOfType<EntryPoint>().EnsureNotNull();
            playerMoveSettings.EnsureNotNull();
            spawn = FindObjectOfType<Spawn>();
            handWeapon.EnsureNotNull();
            
            playerStatistics.EnsureNotNull().changesStatsEvent.AddListener(
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
                        playerMoveSettings.ChangeMoveSettings(true);
                    }
                }
            );
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner)
            {
                input = new KeyBoardInput();
                respawn.RespawnPlayer(spawn.GiveSpawnPoint());
            }
        }

        public void RespawnPlayer()
        {
            if (IsOwner)
            {
                respawn.RespawnPlayer(spawn.GiveSpawnPoint());
            }
        }
        
        private IEnumerator GrabWeaponAsync()
        {
            if (!IsOwner) yield return null;
            yield return new WaitForSecondsRealtime(WaitForGiveWeapon);
            GrabWeaponServerRpc();
        }

        [ServerRpc]
        private void GrabWeaponServerRpc()
        {
            handWeapon.Grab(weaponDictionary.GetWeapon(weaponId.Value));
            GrabWeaponClientRpc();
        }

        [ClientRpc] private void GrabWeaponClientRpc() => handWeapon.Grab(weaponDictionary.GetWeapon(weaponId.Value));

        private void Update()
        {
            if (input.KeyEscape() && IsOwner)
            {
                playerMoveSettings.ChangeMoveSettings();
                entryPoint.menuEvent.Invoke();
            }
        }
    }
}