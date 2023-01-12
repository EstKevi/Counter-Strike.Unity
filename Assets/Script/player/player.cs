using System;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(CharacterController))]

public class player : NetworkBehaviour
{
    [SerializeField] private float speed = 10;
    
    private IInput input = new PlugInput();
    private CharacterController characterController;
    
    private void Awake()
    {
        characterController = GetComponent<CharacterController>().EnsureNotNull();
        input.EnsureNotNull();
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
        var move = new Vector3(input.DirectionX(), 0, input.DirectionZ());
        
        if (IsOwner)
        {
            characterController.Move(move * (speed * Time.deltaTime));
        }
    }
}
