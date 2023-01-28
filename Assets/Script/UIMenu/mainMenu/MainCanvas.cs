using System;
using UnityEngine;
using UnityEngine.Events;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private EntryPoint entryPoint;
    [SerializeField] private WindowHost windowHost;
    [SerializeField] private WindowConnect windowConnect;

    public UnityEvent StartGameHost = new();
    public UnityEvent StartGameClient = new();

    private void Awake()
    {
        entryPoint.EnsureNotNull();
        windowHost.EnsureNotNull();
        windowConnect.EnsureNotNull();
    }

    private void Start()
    {
        StartGameHost.AddListener((() => StartGame(windowHost.IpAddressHost(),ModeGame.Host)));
        
        StartGameClient.AddListener((() => StartGame(windowConnect.IpAddressConnect(),ModeGame.Client)));
    }

    private void StartGame(string address, ModeGame mode)
    {
        entryPoint.ChangeIpAddress(address);
        switch (mode)
        {
            case ModeGame.Host:
                entryPoint.StartGameHost();
                break;
            case ModeGame.Client:
                entryPoint.StartGameClient();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
        gameObject.SetActive(false);
    }
    
    private enum ModeGame
    {
        Host,
        Client
    }
}
