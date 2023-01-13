using Cinemachine;
using Script.player.Inputs;
using Unity.Netcode;
using UnityEngine;

namespace Script.player
{
    public class PlayerCamera : NetworkBehaviour
    {
        [SerializeField] private float speedMouse = 200f;
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

        private int min = -90, max = 90;
        private IInputMouse mouseInput = new PlugMouseInput();
        private float maxValueCameraMouse;
        private float valueMouse;

        private NetworkVariable<float> mouseX = new(
            0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);
        private NetworkVariable<float> mouseY = new(
            0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

        private void Start()
        {
            //Cursor.lockState = CursorLockMode.Locked;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner)
            {
                cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>().EnsureNotNull();
                mouseInput = new KeyMouseInput();
            }
        }

        private void Update()
        {
            if (IsOwner)
            {
                mouseX.Value = mouseInput.DirectionMouseX() * speedMouse * Time.deltaTime;
                mouseY.Value = mouseInput.DirectionMouseY() * speedMouse * Time.deltaTime;
            }
            
            maxValueCameraMouse -= Mathf.Clamp(mouseY.Value, min, max);
            maxValueCameraMouse = Mathf.Clamp(maxValueCameraMouse, min, max);

            valueMouse += mouseX.Value;

            if (IsServer)
            {
                cinemachineVirtualCamera.transform.rotation = Quaternion.Euler(maxValueCameraMouse, valueMouse, 0);
            }
        }
    }
}
