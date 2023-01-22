using Unity.Netcode;
using UnityEngine;

public class DropedGun : NetworkBehaviour
{
    [SerializeField] private GameObject prefabWeapon;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out var player))
        {
            if (prefabWeapon != null)
            {
                player.Grab(prefabWeapon);
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
    }
}