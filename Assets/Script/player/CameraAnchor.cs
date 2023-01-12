using Cinemachine;
using Unity.Netcode;

public class CameraAnchor : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            FindObjectOfType<CinemachineVirtualCamera>().Follow = transform;
        }
    }
}
