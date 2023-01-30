using System;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Events;

namespace Script
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private NetworkManager networkManager;
        [SerializeField] private UnityTransport unityTransport;
        [SerializeField] private PlayerInterface playerInterface;
        [Space] public UnityEvent startGameUnityEvent = new();

        private void Awake()
        {
            playerInterface.EnsureNotNull();
            unityTransport.EnsureNotNull();
            networkManager.EnsureNotNull();
        }

        private void Start()
        {
            startGameUnityEvent.AddListener((() => playerInterface.enabled = false));
        }
        public void StartGame(MainCanvas.ModeGame gameMode)
        {
            switch (gameMode)
            {
                case MainCanvas.ModeGame.Host:
                    networkManager.StartHost();
                    startGameUnityEvent.Invoke();
                break;
                
                case MainCanvas.ModeGame.Client:
                    networkManager.StartClient();
                    startGameUnityEvent.Invoke();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameMode), gameMode, null);
            }
        }

        public void ChangeIpAddress(string address) => unityTransport.ConnectionData.Address = address;

        public void ChangeStats(int ammo, int stock, int heal)
        {
            playerInterface.PlayerStatsSet(ammo, stock, heal);
        }
    }
}