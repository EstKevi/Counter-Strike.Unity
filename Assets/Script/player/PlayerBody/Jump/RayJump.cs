using Unity.Netcode;
using UnityEngine;

namespace Script.player.PlayerBody.Jump
{
    public class RayJump : NetworkBehaviour
    {
        [SerializeField] private float maxDistance;

        [SerializeField] private NetworkVariable<bool> canJump = new(
            false,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);
        private void Update()
        {
            if(!IsOwner) return;
            canJump.Value = Physics.Raycast(transform.position, Vector3.down, out _, maxDistance);
        }

        public bool CanJump() => IsOwner && canJump.Value;
    }
}