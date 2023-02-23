#nullable enable
using Script.Other;
using Cinemachine;
using Script.player.Inputs.Keyboard;
using Script.player.PlayerBody.Jump;
using Unity.Netcode;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Script.player
{
    [RequireComponent(typeof(Player))]
    public class PlayerMove : NetworkBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera Camera = null!;
        [SerializeField] private RayJump jump = null!;
        [SerializeField] private float powerJump;
        [SerializeField] private float speed;
        [SerializeField] private float gravity;
        
        private IInput input = new PlugInput();
        private CharacterController characterController = null!;

        private NetworkVariable<Vector3> move = new(
            Vector3.zero,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);
        
        private void Awake()
        {
            characterController = GetComponent<CharacterController>().EnsureNotNull();
            jump.EnsureNotNull();
        }

        private void Start()
        {
            if(!IsOwner) return;
            if (characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }

            characterController.EnsureNotNull();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if(!IsOwner) return;
            input = new KeyBoardInput();
        }

        private void Update()
        {
            if (IsOwner)
            {
                var x = input.MoveHorizontalX();
                var z = input.MoveVerticalZ();

                move.Value = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0) * new Vector3(x, 0, z);
                if (jump.Jump() && input.KeySpace())
                {
                    JumpPlayer();
                }
            }

            if (IsServer)
            {
                characterController.Move(move.Value * (speed * Time.deltaTime));
                characterController.Move(Vector3.down * (Time.deltaTime * gravity));
            }
        }

        private void JumpPlayer()
        {
            if(IsOwner)
            {
                move.Value += new Vector3(0, powerJump, 0);
            }
        }

        // public void MoveInput()
        // {
        //     if(!IsOwner) return;
        //     input = new KeyBoardInput();
        // }
    }
}