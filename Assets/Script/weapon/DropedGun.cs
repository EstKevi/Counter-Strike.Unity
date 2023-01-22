using Unity.Netcode;
using UnityEngine;

public class DropedGun : NetworkBehaviour
{
    [SerializeField] private GameObject prefabWeapon;

    private void Awake() => prefabWeapon.EnsureNotNull();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<GrabWeapon>(out var hand))
        {
            if (hand.Grab(prefabWeapon))
            {
                Destroy(gameObject);
            }
        }
    }
}