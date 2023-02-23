using Script.Other;
using Script.player.Inputs;
using Script.player.Inputs.Keyboard;
using Script.player.Inputs.Mouse;
using Script.weapon;
using Unity.Netcode;
using UnityEngine;

namespace Script.player.PlayerBody.Hand
{
    public class HandWeapon : NetworkBehaviour
    {
        [SerializeField] private PlayerRaycast playerRaycast;
        [SerializeField] private GameObject hand;
        // private GameObject weaponObject;
        private IInputMouse inputMouse = new PlugMouseInput();
        private IInput keyBoardInput = new PlugInput();
        private IGun weaponInHand;

        public int WeaponAmmo { get; private set; }
        public int WeaponStock { get; private set; }

        private void Awake()
        {
            hand.EnsureNotNull();
            playerRaycast.EnsureNotNull();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (!IsOwner) return;
            
            inputMouse = new KeyMouseInput();
            keyBoardInput = new KeyBoardInput();
        }
        
        public bool Grab(GameObject weaponPrefab)
        {
            if (weaponInHand != null) return false;
            hand = Instantiate(weaponPrefab, hand.transform);
            hand.TryGetComponent(out weaponInHand);
            return true;
        }

        public bool ApplyStockBonus(int ammo)
        {
            if (weaponInHand == null) return false;
            weaponInHand.Stock += ammo;
            return true;
        }

        [ServerRpc]
        private void ShootServerRpc()
        {
            weaponInHand.Shoot(playerRaycast.HitCollider);
            ShootClientRpc();
        }

        [ServerRpc]
        private void ReloadServerRpc()
        {
            weaponInHand.Reload();
            ReloadClientRpc();
        }

        [ClientRpc] private void ReloadClientRpc() => weaponInHand.Reload();
        [ClientRpc] private void ShootClientRpc() => weaponInHand.Shoot(playerRaycast.HitCollider);

        private void Update()
        {
            if (weaponInHand == null || !IsOwner) return;
            
            if (inputMouse.MouseLeftButton())
            {
                ShootServerRpc();
            }
            
            if (keyBoardInput.KeyR())
            {
                ReloadServerRpc();
            }
            
            WeaponAmmo = weaponInHand.Ammo;
            WeaponStock = weaponInHand.Stock;
        }
    }
}