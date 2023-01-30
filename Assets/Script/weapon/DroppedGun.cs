using Script.player.Player.Hand;
using Unity.Netcode;
using UnityEngine;

namespace Script.weapon
{
    public class DroppedGun : NetworkBehaviour
    {
        [SerializeField] private GameObject prefabWeapon;

        private void Awake() => prefabWeapon.EnsureNotNull();
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<HandWeapon>(out var hand) || hand == null) return;
            if (hand.Grab(prefabWeapon) && IsOwner)
            {
                Destroy(gameObject);
            }
        }
    }
}