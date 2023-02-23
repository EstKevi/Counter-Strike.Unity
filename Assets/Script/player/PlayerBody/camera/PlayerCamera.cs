using Cinemachine;
using Script.Other;
using Script.player.Inputs;
using Script.player.Inputs.Mouse;
using Unity.Netcode;
using UnityEngine;

namespace Script.player.PlayerBody.camera
{
    public class PlayerCamera : NetworkBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        [SerializeField] private float speedMouse = 1000;
        private IInputMouse mouseInput = new PlugMouseInput();
        private int minRotateCamera = -90, maxRotateCamera = 90;

        private NetworkVariable<float> mouseValueX = new(
            0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);
        
        private NetworkVariable<float> mouseValueY = new(
            0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

        private void Awake() => cinemachineVirtualCamera.EnsureNotNull();

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (!IsOwner)
            {
                cinemachineVirtualCamera.enabled = false;
            }
            //todo
            // else
            // {
            //     mouseInput = new KeyMouseInput();
            //     Cursor.lockState = CursorLockMode.Locked;
            // }
        }

        private void Update()
        {
            if (IsOwner)
            {
                float x = mouseInput.DirectionMouseX() * speedMouse * Time.deltaTime;
                float y = mouseInput.DirectionMouseY() * speedMouse * Time.deltaTime;

                mouseValueX.Value -= y;
                mouseValueY.Value += x;

                mouseValueX.Value = Mathf.Clamp(mouseValueX.Value, minRotateCamera, maxRotateCamera);
            }

            if (IsServer)
            {
                transform.rotation = Quaternion.Euler(mouseValueX.Value, mouseValueY.Value, 0);
            }
        }

        // public void MoveInput()
        // {
        //     if(!IsOwner) return;
        //     mouseInput = new KeyMouseInput();
        //     Cursor.lockState = CursorLockMode.Locked;
        // }
    }
}

