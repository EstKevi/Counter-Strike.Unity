using Script.Other;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UIMenu.mainMenu.WindowsUI
{
    [RequireComponent(typeof(OnDragWindow))]
    public class WindowSettings : MonoBehaviour
    {
        [SerializeField] private Button closeButton;

        private void Awake() => closeButton.EnsureNotNull().onClick.AddListener(() => gameObject.SetActive(false));
    }
}
