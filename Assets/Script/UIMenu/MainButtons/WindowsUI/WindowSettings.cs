using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowSettings : MonoBehaviour , IDragHandler
{
    [SerializeField] private Button closeButton;

    private void Awake() => closeButton.EnsureNotNull().onClick.AddListener(() => gameObject.SetActive(false));
    

    public void OnDrag(PointerEventData eventData)
    {
        //TODO перемещение курсором мыши
    }
}
