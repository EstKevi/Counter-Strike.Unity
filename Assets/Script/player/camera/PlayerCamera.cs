using Cinemachine;
using Script.Other;
using Script.player.Inputs;
using Script.player.Inputs.Mouse;
using Unity.Netcode;
using UnityEngine;

namespace Script.player.camera
{
    public class PlayerCamera : NetworkBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        [SerializeField] private float speedMouse = 1000;
        private IInputMouse mouseInput = new PlugMouseInput();
        private int min = -90, max = 90;

        private NetworkVariable<float> mouseValueX = new(
            0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);
        
        private NetworkVariable<float> mouseValueY = new(
            0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

        private void Awake() => cinemachineVirtualCamera.EnsureNotNull();
        private void Start() => Cursor.lockState = CursorLockMode.Locked;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner)
            {
                mouseInput = new KeyMouseInput();
            }
            else
            {
                cinemachineVirtualCamera.enabled = false;
            }
        }

        private void Update()
        {
            if (IsOwner)
            {
                var x = mouseInput.DirectionMouseX() * speedMouse * Time.deltaTime;
                var y = mouseInput.DirectionMouseY() * speedMouse * Time.deltaTime;

                mouseValueX.Value -= y;
                mouseValueY.Value += x;

                mouseValueX.Value = Mathf.Clamp(mouseValueX.Value, min, max);
            }

            if (IsServer)
            {
                transform.rotation = Quaternion.Euler(mouseValueX.Value, mouseValueY.Value, 0);
            }
        }
    }
}

