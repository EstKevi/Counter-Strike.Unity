using System;
using Script.UIMenu.playerCanvas;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Other
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private NetworkManager networkManager;
        [SerializeField] private UnityTransport unityTransport;
        [SerializeField] private MainCanvas mainCanvas;
        [SerializeField] private PlayerInterface playerInterface;
        [Space] public UnityEvent startGameUnityEvent = new();
        
        
        private void Awake()
        {
            playerInterface.EnsureNotNull();
            unityTransport.EnsureNotNull();
            networkManager.EnsureNotNull();
            mainCanvas.EnsureNotNull();
        }

        private void Start()
        {
            startGameUnityEvent.AddListener(() =>
            {
                playerInterface.gameObject.SetActive(true);
                mainCanvas.gameObject.SetActive(false);
            });
        }

        public void StartGame(MainCanvas.ModeGame gameMode)
        {
            switch (gameMode)
            {
                case MainCanvas.ModeGame.Host:
                    networkManager.StartHost();
                    break;

                case MainCanvas.ModeGame.Client:
                    networkManager.StartClient();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(gameMode), gameMode, null);
            }
            startGameUnityEvent.Invoke();
        }

        public void ChangeIpAddress(string address) => unityTransport.ConnectionData.Address = address;

        public void ChangeStats(int heal, int ammo, int stock)
        {
            playerInterface.PlayerStatsSet(heal, ammo, stock);
        }
    }
}