using System;
using System.Numerics;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(CharacterController))]

public class player : NetworkBehaviour
{
    [SerializeField] private float speed = 10;
    
    private IInput input = new PlugInput();
    private CharacterController characterController;

    private NetworkVariable<Vector3> move = new(
        Vector3.zero,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    private void Awake()
    {
        characterController = GetComponent<CharacterController>().EnsureNotNull();
    }

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
            var directionVector = new Vector3(input.DirectionX(), 0, input.DirectionZ());

            move.Value = directionVector;
        }
        
        if (IsServer)
        {
            characterController.Move(move.Value * (speed * Time.deltaTime));
        }
    }
}
