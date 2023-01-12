using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject player;
    private void Awake()
    {
        networkManager = FindObjectOfType<NetworkManager>().EnsureNotNull();

        player.EnsureNotNull();
    }

    public void Host()
    {
        networkManager.StartHost();
    }

    public void Client()
    {
        networkManager.StartClient();
    }
}