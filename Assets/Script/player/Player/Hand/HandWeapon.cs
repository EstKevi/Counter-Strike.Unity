using Script.Other;
using Script.player.Inputs;
using Script.player.Inputs.Keyboard;
using Script.player.Inputs.Mouse;
using Script.player.Player.camera;
using Script.weapon;
using Unity.Netcode;
using UnityEngine;

namespace Script.player.Player.Hand
{
    public class HandWeapon : NetworkBehaviour
    {
        [SerializeField] private PlayerCamera playerCamera;
        [SerializeField] private GameObject hand;
        [SerializeField] private bool canGrab = true;

        private IInputMouse inputMouse = new PlugMouseInput();
        private IInput keyBoardInput = new PlugInput();
        private IGun weapon;

        public int WeaponAmmo { get; private set; }
        public int WeaponStock { get; internal set; }

        private void Awake()
        {
            hand.EnsureNotNull();
            playerCamera.EnsureNotNull();
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
            if (canGrab)
            {
                hand = Instantiate(weaponPrefab, hand.transform);
                hand.TryGetComponent<IGun>(out weapon);

                canGrab = false;
            
                return !canGrab;
            }

            return false;
        }

        [ServerRpc] private void ShootServerRpc() => weapon.Shoot(playerCamera.HitCollider);
        private void Update()
        {
            if (weapon == null || !IsOwner) return;
            
            if (inputMouse.MouseLeftButton())
            {
                ShootServerRpc();
            }

            if (keyBoardInput.KeyR())
            {
                weapon.Reload();
            }

            WeaponAmmo = weapon.Ammo;
            WeaponStock = weapon.Stock;
        }
    }
}