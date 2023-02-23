using Script.Other;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UIMenu.mainMenu.WindowsUI
{
    [RequireComponent(typeof(OnDragWindow))]
    public class WindowConnect : MonoBehaviour
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Button connectButton;
        [SerializeField] private TMP_InputField enterIP;
        [SerializeField] private MainCanvas mainCanvas;
    
        private string ipAddressText;
        public string IpAddressConnect() => ipAddressText;

        private void Awake()
        {
            mainCanvas.EnsureNotNull();
            closeButton.EnsureNotNull().onClick.AddListener((() => gameObject.SetActive(false)));
            connectButton.EnsureNotNull().onClick.AddListener((() => mainCanvas.StartGameClient.Invoke()));
            enterIP.EnsureNotNull().onValueChanged.AddListener((text => ipAddressText = text));
        }
    }
}
