using UnityEngine;

namespace Script.player
{
    public class PlayerRaycast : MonoBehaviour
    {
        [Header("debug")] [SerializeField] private Collider hitCollider;
        public Collider HitCollider => hitCollider;

        private void Update()
        {
            Physics.Raycast(transform.position, transform.forward, out var hit);
            hitCollider = hit.collider;
        }
    }
}