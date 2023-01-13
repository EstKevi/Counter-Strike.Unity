using Cinemachine;
using Unity.Netcode;

public class CameraAnchor : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        FindObjectOfType<CinemachineVirtualCamera>().EnsureNotNull().Follow = transform;
    }
}