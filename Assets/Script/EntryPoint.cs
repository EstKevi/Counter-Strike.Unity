using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Events;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private UnityTransport unityTransport;
    [SerializeField] private GameObject playerInterface;
    [Space] public UnityEvent startGameUnityEvent = new();

    private void Awake()
    {
        playerInterface.EnsureNotNull();
        unityTransport.EnsureNotNull();
        networkManager.EnsureNotNull();
    }

    private void Start()
    {
        startGameUnityEvent.AddListener((() => playerInterface.SetActive(true)));
    }

    public void StartGameHost()
    {
        networkManager.StartHost();
        startGameUnityEvent.Invoke();
    }

    public void StartGameClient()
    {
        networkManager.StartClient();
        startGameUnityEvent.Invoke();
    }
    public void ChangeIpAddress(string address) => unityTransport.ConnectionData.Address = address;
}