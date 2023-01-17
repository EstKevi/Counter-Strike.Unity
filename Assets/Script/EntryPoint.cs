using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private UnityTransport unityTransport;

    private void Awake()
    {
        unityTransport = FindObjectOfType<UnityTransport>().EnsureNotNull();
        networkManager = FindObjectOfType<NetworkManager>().EnsureNotNull();
    }

    public void StartGameHost() => networkManager.StartHost();
    public void StartGameClient() => networkManager.StartClient();
    public void ChangeIpAddress(string address) => unityTransport.ConnectionData.Address = address;
}