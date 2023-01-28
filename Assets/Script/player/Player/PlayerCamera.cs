using System;
using Cinemachine;
using Script.player.Inputs;
using Unity.Netcode;
using UnityEngine;

namespace Script.player
{
    public class PlayerCamera : NetworkBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        [SerializeField] private float speedMouse = 1000;
        private IInputMouse mouseInput = new PlugMouseInput();
        private int min = -90, max = 90;
        
        private NetworkVariable<float> valueMouseY = new(
            0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);
        
        private NetworkVariable<float> valueMouseX = new(
            0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

        private void Start() => Cursor.lockState = CursorLockMode.Locked;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner) mouseInput = new KeyMouseInput();
            if(!IsOwner) cinemachineVirtualCamera.enabled = false;
        }

        private void Update()
        {
            if (IsOwner)
            {
                var x = mouseInput.DirectionMouseX() * speedMouse * Time.deltaTime;
                var y = mouseInput.DirectionMouseY() * speedMouse * Time.deltaTime;

                valueMouseY.Value += x;
                valueMouseX.Value -= y;
                
                valueMouseX.Value = Mathf.Clamp(valueMouseX.Value, min, max);
            }

            if (IsServer)
            {
                transform.rotation = Quaternion.Euler(valueMouseX.Value, valueMouseY.Value, 0);
            }
        }
    }
}