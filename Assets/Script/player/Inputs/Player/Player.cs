using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(CharacterController))]

public class Player : NetworkBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float gravity;
    
    private IInput input = new PlugInput();
    private CharacterController characterController;

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
            
            move.Value = transform.rotation * new Vector3(x,0,z);
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
