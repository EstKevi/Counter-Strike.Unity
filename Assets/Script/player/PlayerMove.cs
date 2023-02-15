#nullable enable
using Cinemachine;
using Script.Other;
using Script.player.Inputs.Keyboard;
using Unity.Netcode;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Script.player
{
    [RequireComponent(typeof(Player))]
    public class PlayerMove : NetworkBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cineCamera = null!;
        [SerializeField] private float speed;
        [SerializeField] private float gravity;

        private IInput input = new PlugInput();
        private CharacterController characterController = null!;

        private NetworkVariable<Vector3> move = new(
            Vector3.zero,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);
        
        private void Awake() => characterController = GetComponent<CharacterController>().EnsureNotNull();

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner)
            {
                input = new KeyBoardInput();
            }
        }

        private void Update()
        {
            if (IsOwner)
            {
                var x = input.MoveHorizontalX();
                var z = input.MoveVerticalZ();
                
                move.Value = new Vector3(x, 0, z);
                move.Value = Quaternion.Euler(0, cineCamera.transform.rotation.eulerAngles.y, 0) * move.Value;
            }

            if (IsServer)
            {
                characterController.Move(move.Value * (speed * Time.deltaTime));
                characterController.Move(Vector3.down * (Time.deltaTime * gravity));
            }
        }

        private void Jump()
        {
            /*TODO сделать прыжок */
        }
    }
}