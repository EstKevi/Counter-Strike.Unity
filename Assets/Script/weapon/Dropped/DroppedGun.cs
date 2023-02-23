using System;
    using Script.Other;
using Script.player.PlayerBody.Hand;
using Unity.Netcode.Components;
    using UnityEngine;

namespace Script.weapon.Dropped
{
    [RequireComponent(typeof(NetworkRigidbody))]
    [RequireComponent(typeof(NetworkTransform))]
    [RequireComponent(typeof(Collider))]
    public class DroppedGun : MonoBehaviour
    {
        [SerializeField] private GameObject prefabWeapon;

        private void Awake()
        {
            prefabWeapon.EnsureNotNull();
            var collider = GetComponent<Collider>().EnsureNotNull();
            if (collider.isTrigger is false)
            {
                throw new Exception("must be trigger");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<HandWeapon>(out var hand) || hand == null) return;

            if (hand.Grab(prefabWeapon))
            {
                Destroy(gameObject);
            }
        }
    }
}