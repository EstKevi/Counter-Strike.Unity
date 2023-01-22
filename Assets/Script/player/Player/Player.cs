#nullable enable
using Cinemachine;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(CharacterController))]

public class Player : NetworkBehaviour
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

    private void Awake()
    {
        input.EnsureNotNull();
        characterController = GetComponent<CharacterController>().EnsureNotNull();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            input = new KeyBoardInput().EnsureNotNull();
        }
    }

    private void Update()
    {
        if (IsOwner)
        {
            var x = input.DirectionX();
            var z = input.DirectionZ();

            move.Value = new Vector3(x,0,z);
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
        //TODO сделать прыжок
    }
}
