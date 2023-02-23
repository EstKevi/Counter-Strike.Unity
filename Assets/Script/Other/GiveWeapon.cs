using System.Collections;
using Script.player.PlayerBody.Hand;
using Unity.Netcode;
using UnityEngine;

namespace Script.Other
{
    public class GiveWeapon : NetworkBehaviour
    {
        [SerializeField] private WeaponDictionary weaponDictionary;
        [SerializeField] private HandWeapon handWeapon;
        [SerializeField] private NetworkVariable<int> weaponCode = new(
            0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

        private void Start()
        {
            weaponDictionary = FindObjectOfType<WeaponDictionary>().EnsureNotNull();
            handWeapon.EnsureNotNull();
        }

        private void Update()
        {
            if (!IsOwner) return;
            
            if (Input.GetKey(KeyCode.L))
            {
                weaponCode.Value = 0;
                SpawnWeaponServerRpc();
            }

            if (Input.GetKey(KeyCode.K))
            {
                weaponCode.Value = 1;
                SpawnWeaponServerRpc();
            }
        }

        [ServerRpc]
        private void SpawnWeaponServerRpc()
        {
            StartCoroutine(GrabWeaponAsync());
        }
        
        [ClientRpc] private void SpawnWeaponClientRpc() => handWeapon.Grab(weaponDictionary.GetWeapon(weaponCode.Value));

        private IEnumerator GrabWeaponAsync()
        {
            if (!IsOwner) yield return null;
            yield return null;
            handWeapon.Grab(weaponDictionary.GetWeapon(weaponCode.Value));
            SpawnWeaponClientRpc();
        }
    }
}
