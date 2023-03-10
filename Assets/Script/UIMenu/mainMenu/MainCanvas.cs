using System;
using Script.Other;
using Script.UIMenu.mainMenu.WindowsUI;
using UnityEngine;
using UnityEngine.Events;

namespace Script.UIMenu.mainMenu
{
    public class MainCanvas : MonoBehaviour
    {
        [SerializeField] private EntryPoint entryPoint;
        [SerializeField] private WindowHost windowHost;
        [SerializeField] private WindowConnect windowConnect;

        public EntryPoint EntryPoint => entryPoint;

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
            StartGameHost.AddListener(() => 
                StartGame(
                    windowHost.IpAddressHost(),
                    ModeGame.Host)
            );
        
            StartGameClient.AddListener(() =>
                StartGame(
                    windowConnect.IpAddressConnect(), 
                    ModeGame.Client)
            );
        }

        private void StartGame(string address, ModeGame mode)
        {
            entryPoint.ChangeIpAddress(address);
            switch (mode)
            {
                case ModeGame.Host:
                    entryPoint.StartGame(ModeGame.Host);
                    break;
            
                case ModeGame.Client:
                    entryPoint.StartGame(ModeGame.Client);
                    break;
            
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    
        public enum ModeGame
        {
            Host,
            Client
        }
    }
}
