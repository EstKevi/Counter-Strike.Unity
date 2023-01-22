using UnityEngine;

public class DropedGun : MonoBehaviour
{
    [SerializeField] private GameObject prefabWeapon;

    private void Awake() => prefabWeapon.EnsureNotNull();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<HandWeapon>(out var hand))
        {
            if (hand.Grab(prefabWeapon))
            {
                Destroy(gameObject);
            }
        }
    }
}