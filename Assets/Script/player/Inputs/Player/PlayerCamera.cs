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

        private int min = -90, max = 90;
        private IInputMouse mouseInput = new PlugMouseInput();

        private float maxValueCameraMouse;
        private float valueXMouse;

        private NetworkVariable<float> valueMouseX = new(
            0,
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

        private void Start() => Cursor.lockState = CursorLockMode.Locked;


        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            if (IsOwner)
                mouseInput = new KeyMouseInput().EnsureNotNull();
            else
                cinemachineVirtualCamera.enabled = false;

        }

        private void Update()
        {
            if (IsOwner)
            {
                valueMouseX.Value = mouseInput.DirectionMouseX() * speedMouse * Time.deltaTime;
                var y = mouseInput.DirectionMouseY() * speedMouse * Time.deltaTime;

                maxValueCameraMouse -= Mathf.Clamp(y, min, max);
                maxValueCameraMouse = Mathf.Clamp(maxValueCameraMouse, min, max);

                valueXMouse += valueMouseX.Value;

                cinemachineVirtualCamera.transform.rotation = Quaternion.Euler(maxValueCameraMouse, valueXMouse, 0);
            }

            if (IsServer)
            {
                transform.Rotate(Vector3.up * valueMouseX.Value);
            }
        }
    }
}