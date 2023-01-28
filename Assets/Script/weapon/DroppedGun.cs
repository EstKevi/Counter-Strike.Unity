using Unity.Netcode;
using UnityEngine;

namespace Script.weapon
{
    public class DroppedGun : NetworkBehaviour
    {
        [SerializeField] private GameObject prefabWeapon;

        private void Awake() => prefabWeapon.EnsureNotNull();

        [ServerRpc]
        private void DestroyServerRpc()
        {
            Destroy(gameObject);
            DestroyClientRpc();
        }
        
        [ClientRpc] private void DestroyClientRpc() => Destroy(gameObject);
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent<HandWeapon>(out var hand) || hand == null) return;
            
            if (hand.Grab(prefabWeapon) && IsOwner)
            {
                DestroyServerRpc();
            }
        }
    }
}