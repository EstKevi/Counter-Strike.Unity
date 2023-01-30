using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Script.UIMenu.mainMenu.WindowsUI
{
    public class WindowSettings : MonoBehaviour , IDragHandler
    {
        [SerializeField] private Button closeButton;

        private void Awake() => closeButton.EnsureNotNull().onClick.AddListener(() => gameObject.SetActive(false));
    

        public void OnDrag(PointerEventData eventData)
        {
            //TODO перемещение курсором мыши
        }
    }
}
