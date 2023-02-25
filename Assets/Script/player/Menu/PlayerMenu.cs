using Script.player.PlayerBody.camera;
using Script.player.PlayerBody.Hand;
using Unity.Netcode;
using UnityEngine;

namespace Script.player.Menu
{
    public class PlayerMenu : NetworkBehaviour
    {
        [SerializeField] private PlayerCamera playerCamera;
        [SerializeField] private PlayerMove playerMove;
        [SerializeField] private HandWeapon handWeapon;

        private void Start() => Menu(false);
        private void LockedCursor(bool active)
        {
            if (!IsOwner) return;
            Cursor.lockState = active ? CursorLockMode.Locked : CursorLockMode.None;
        }
        
        public void Menu()
        {
            if(!IsOwner) return;
            playerCamera.enabled = !playerCamera.enabled;
            playerMove.enabled = !playerMove.enabled;
            handWeapon.enabled = !handWeapon.enabled;
            LockedCursor(playerCamera.enabled);
        }
        
        public void Menu(bool active)
        {
            if(!IsOwner) return;
            playerCamera.enabled = active;
            playerMove.enabled = active;
            handWeapon.enabled = active;
            LockedCursor(active);
        }
    }
}
