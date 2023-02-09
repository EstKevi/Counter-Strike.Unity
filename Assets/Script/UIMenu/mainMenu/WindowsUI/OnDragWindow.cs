using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.UIMenu.mainMenu.WindowsUI
{
    public class OnDragWindow : MonoBehaviour, IDragHandler, IPointerDownHandler
    {
        private const int lastPosition = -1;

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
            //TODO сделать перемещение не от центра 
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.SetSiblingIndex(lastPosition);
        }
    }
}