using System;
using Script.UIMenu.mainMenu;
using Script.UIMenu.playerCanvas;
using Script.UIMenu.WeaponCanvas;
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
        [SerializeField] private WeaponCanvas weaponCanvas;
        public UnityEvent startGameUnityEvent = new();
        public UnityEvent<int> chooseWeapon = new();
        public UnityEvent menuEvent = new();

        public NetworkManager EntryManager => networkManager;

        private void Awake()
        {
            playerInterface.EnsureNotNull();
            unityTransport.EnsureNotNull();
            networkManager.EnsureNotNull();
            weaponCanvas.EnsureNotNull();
            mainCanvas.EnsureNotNull();
        }

        private void Start()
        {
            startGameUnityEvent.AddListener(() =>
                {
                    weaponCanvas.gameObject.SetActive(true);
                    mainCanvas.gameObject.SetActive(false);
                }
            );

            weaponCanvas.chooseUiWeapon.AddListener(arg0 =>
                {
                    chooseWeapon.Invoke(arg0);
                    weaponCanvas.gameObject.SetActive(false);
                    playerInterface.gameObject.SetActive(true);
                }
            );

            menuEvent.AddListener(() =>
                {
                    playerInterface.gameObject.SetActive(!playerInterface.gameObject.activeSelf);
                    mainCanvas.gameObject.SetActive(!mainCanvas.gameObject.activeSelf);
                }
            );
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

        public void ChangeStats(int heal, int ammo, int stock) => playerInterface.PlayerStatsSet(heal, ammo, stock);
    }
}