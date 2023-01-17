// using Cinemachine;
// using Unity.Netcode;
// using UnityEngine;
//
// public class CameraAnchor : NetworkBehaviour
// {
//     public override void OnNetworkSpawn()
//     {
//         base.OnNetworkSpawn();
//         cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>().EnsureNotNull();
//         cinemachineVirtualCamera.Follow = transform;
//     }
// }